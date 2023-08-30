namespace Fanex.Module.RNGCasino.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using Alpha.IPInfo.Services;
    using Common;
    using Criteria;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.Caching;
    using Fanex.Result.Core.GlobalGaming;
    using Fanex.Result.Core.GlobalGaming.DTO;
    using Fanex.Tarus.Infrastructure.Configurations;
    using Helpers;
    using Logging.Extensions.Web;
    using Models;
    using Models.HandHistory;
    using Newtonsoft.Json;
    using Repositories;
    using Resources;
    using Result.Core.Voidbridge;
    using Sunplus.Accounting.Business;
    using Sunplus.Accounting.Core.Common;
    using Sunplus.Accounting.Core.Extensions;
    using Sunplus.Accounting.DataAccess.Report;

    public class RNGCasinoService : IRNGCasinoService
    {
        private readonly IRNGCasinoRepository _rngCasinoRepository;
        private readonly IIPInfoService _ipInfoService;
        private readonly ICacheService _cacheService;
        private readonly Alpha.IPInfo.DataAccess.IIPInfoDataAccess _iPInfoDataAccess;

        public RNGCasinoService()
            : this(null)
        {
            _cacheService = new CacheService();
            _iPInfoDataAccess = new IPInfoDataAccess();

            _ipInfoService = new IPInfoService(new Alpha.IPInfo.Models.IPInformationConfig
            {
                IPV4TopCountries = AppSettings.IPV4TopCountries,
                IPV4TopCountriesCacheInMinutes = AppSettings.IPV4TopCountriesCacheInMinutes,
                IPV4CacheInMinutes = AppSettings.IPV4CacheInMinutes,
                IPV6CacheInMinutes = AppSettings.IPV6CacheInMinutes
            }, _cacheService, _iPInfoDataAccess);

        }

        public RNGCasinoService(IRNGCasinoRepository rngCasinoRepository)
        {
            _rngCasinoRepository = rngCasinoRepository ?? new RNGCasinoRepository();
        }

        public IEnumerable<TicketDetail> BetDetails(GetRNGCasinoBetDetailsCriteria mainCriteria)
        {
            var detailsTickets = new List<TicketDetail>();
            var siteDB = ObjectDbExtensions.GetSiteDBConfigByCustId(mainCriteria.CustId);
            if (siteDB != null)
            {
                var tickets = new List<Ticket>();
                var siteDBRow = 0;
                if (siteDB.IsSiteDB && mainCriteria.WinLossDate > ReportUtils.BalanceUp.MoveBetsDay)
                {
                    var siteDBCriteria = new GetRNGCasinoBetDetailsInSiteDBCriteria
                    {
                        CustId = mainCriteria.CustId,
                        PageIndex = mainCriteria.PageIndex,
                        PageSize = mainCriteria.PageSize,
                        TransId = mainCriteria.TransId,
                        TransType = mainCriteria.TransType
                    };
                    tickets = _rngCasinoRepository.BetDetails(siteDBCriteria, siteDB.AGIP).ToList();
                    mainCriteria.TotalRows = siteDBCriteria.TotalRows;
                }
                if (!siteDB.IsSiteDB ||
                    (siteDB.IsSiteDB && mainCriteria.WinLossDate <= ReportUtils.BalanceUp.MoveBetsDay))
                {
                    mainCriteria.ExcludeBettrans = siteDB.IsSiteDB;
                    tickets = _rngCasinoRepository.BetDetails(mainCriteria).ToList();
                }

                var ticketHelper = new TicketHelper();
                var factory = new BetListHTMLFactory();
                var builder = factory.CreateBuilder();
                var betListHelper = new BetListHelper();
                GetCachePropertyById funcGetOddsTypeName = ticketHelper.GetOddsTypeNameById;

                List<ITicketData> outputLiveCasino = new List<ITicketData>();
                foreach (var ticket in tickets)
                {
                    var ticketDetail = new TicketDetail();
                    ticket.CustId = mainCriteria.CustId;
                    ticketDetail.Ticket = ticket;
                    ticketHelper.IsShowOddsWithChoice = betListHelper.IsShowOddsWithChoice(ticketDetail.Ticket.BetTypeId);
                    ticketDetail.TransDateHtml = builder.BuildTrans(ticketDetail.Ticket);
                    ticketDetail.ChoiceHtml = builder.BuildChoice(ticketDetail.Ticket, ticketHelper, outputLiveCasino, false);
                    ticketDetail.StakeHtml = builder.BuildStake(ticketDetail.Ticket, ticketHelper);
                    ticketDetail.OddHtml = builder.BuildOdds(ticketDetail.Ticket, outputLiveCasino, funcGetOddsTypeName);
                    ticketDetail.StatusHtml = betListHelper.BuildStatus(builder, ticketHelper, ticketDetail.Ticket, new List<ITicketData>());
                    ticketDetail.IP = ticketDetail.Ticket.IP;
                    detailsTickets.Add(ticketDetail);
                }
            }
            AssignCountry(detailsTickets);

            return detailsTickets;
        }

        public HandHistoryModel GetResultCriteria(GetResultCriteria criteria, string language)
        {
            HandHistoryModel result = new HandHistoryModel();
            result.ActionInfo = new ResultDetail();
            try
            {
                GlobalGamingResultApi globalGamingResultApi = new GlobalGamingResultApi();
                int siteId = 3200;
                switch (criteria.RoleId)
                {
                    case 3:
                        siteId = 3100;
                        break;

                    case 2:
                        siteId = 3000;
                        break;
                }

                result.Iframe = globalGamingResultApi.GetResultUrl(new GlobalGamingDTO
                {
                    CustId = criteria.CustId.ToString(),
                    GameId = criteria.GameplayId,
                    SiteId = siteId.ToString(),
                    Language = language
                });
            }
            catch (Exception ex)
            {
                result.ActionInfo = new ResultDetail
                {
                    ResultCode = 1,
                    ResultMessage = ex.Message
                };
            }

            return result;
        }

        public IEnumerable<TicketDetail> JackpotDetails(GetJackpotDetailsCriteria criteria)
        {
            var detailsTickets = new List<TicketDetail>();
            var siteDB = ObjectDbExtensions.GetSiteDBConfigByCustId(criteria.CustId);
            if (siteDB != null)
            {
                var tickets = new List<Ticket>();
                if (siteDB.IsSiteDB && criteria.WinLostDate > ReportUtils.BalanceUp.MoveBetsDay)
                {
                    var siteDBCriteria = new GetJackpotDetailsInSiteDBCriteria
                    {
                        BetType = criteria.BetType,
                        CustId = criteria.CustId,
                        Title = criteria.Title,
                        TransId = criteria.TransId
                    };
                    tickets = _rngCasinoRepository.JackpotDetails(siteDBCriteria, siteDB.AGIP).ToList();
                }

                if (!siteDB.IsSiteDB ||
                    (siteDB.IsSiteDB && criteria.WinLostDate <= ReportUtils.BalanceUp.MoveBetsDay))
                {
                    criteria.ExcludeBettrans = siteDB.IsSiteDB;
                    tickets = _rngCasinoRepository.JackpotDetails(criteria).ToList();
                }
                var ticketHelper = new TicketHelper();
                var factory = new BetListHTMLFactory();
                var builder = factory.CreateBuilder();
                var betListHelper = new BetListHelper();
                GetCachePropertyById funcGetOddsTypeName = ticketHelper.GetOddsTypeNameById;

                var outputLiveCasino = new List<ITicketData>();
                foreach (var ticket in tickets)
                {
                    var ticketDetail = new TicketDetail();
                    ticket.CustId = criteria.CustId;
                    ticketDetail.Ticket = ticket;
                    ticketHelper.IsShowOddsWithChoice = betListHelper.IsShowOddsWithChoice(ticketDetail.Ticket.BetTypeId);
                    ticketDetail.TransDateHtml = builder.BuildTrans(ticketDetail.Ticket);
                    ticketDetail.ChoiceHtml = builder.BuildChoice(ticketDetail.Ticket, ticketHelper, outputLiveCasino, false);
                    ticketDetail.StakeHtml = builder.BuildStake(ticketDetail.Ticket, ticketHelper);
                    ticketDetail.OddHtml = builder.BuildOdds(ticketDetail.Ticket, outputLiveCasino, funcGetOddsTypeName);
                    ticketDetail.StatusHtml = betListHelper.BuildStatus(builder, ticketHelper, ticketDetail.Ticket, new List<ITicketData>());
                    ticketDetail.IP = ticketDetail.Ticket.IP;

                    detailsTickets.Add(ticketDetail);
                }
            }
            AssignCountry(detailsTickets);
            return detailsTickets;
        }

        public async Task AssignCountry(IEnumerable<TicketDetail> tickets)
        {
            var ipList = tickets
             .Where(t => !string.IsNullOrWhiteSpace(t.IP) && (AppSession.ViewIPVN || !IpManagement.IsVNIP(t.IP.Trim())))
             .Select(t => t.IP.Trim())
             .Distinct();
            var countryDictionary = await GetCountries(ipList);

            string country;
            foreach (var item in tickets)
            {
                if (!string.IsNullOrWhiteSpace(item.IP)
                    && countryDictionary.TryGetValue(item.IP.Trim(), out country))
                {
                    ((TicketDetail)item).Country = country;
                }
            }
        }

        private async Task<IDictionary<string, string>> GetCountries(IEnumerable<string> ipList)
        {
            try
            {
                return await _ipInfoService.GetCountries(ipList);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }

            return new Dictionary<string, string>();
        }

    }
}
