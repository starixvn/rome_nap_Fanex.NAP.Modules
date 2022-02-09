namespace Fanex.Module.RNGCasino.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Web;
    using Common;
    using Criteria;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.Result.Core.GlobalGaming;
    using Fanex.Result.Core.GlobalGaming.DTO;
    using Helpers;
    using Logging.Extensions.Web;
    using Models;
    using Models.HandHistory;
    using Newtonsoft.Json;
    using Repositories;
    using Resources;
    using Result.Core.Voidbridge;

    public class RNGCasinoService : IRNGCasinoService
    {
        private readonly IRNGCasinoRepository _rngCasinoRepository;

        public RNGCasinoService()
            : this(null)
        {
        }

        public RNGCasinoService(IRNGCasinoRepository rngCasinoRepository)
        {
            _rngCasinoRepository = rngCasinoRepository ?? new RNGCasinoRepository();
        }

        public IEnumerable<TicketDetail> BetDetails(GetRNGCasinoBetDetailsCriteria criteria)
        {
            var rawData = _rngCasinoRepository.BetDetails(criteria);
            var detailsTickets = new List<TicketDetail>();
            if (rawData != null)
            {
                ITicketHelper ticketHelper = new TicketHelper();
                BetListHTMLFactory factory = new BetListHTMLFactory();
                BetListHTMLBuilder builder = factory.CreateBuilder();
                BetListHelper betListHelper = new BetListHelper();
                GetCachePropertyById funcGetOddsTypeName = ticketHelper.GetOddsTypeNameById;

                List<ITicketData> outputLiveCasino = new List<ITicketData>();
                foreach (var ticket in rawData)
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
                    detailsTickets.Add(ticketDetail);
                }
            }

            return detailsTickets;
        }
        
        public HandHistoryModel GetVoidbridgeResult(GetVoidbridgeResultCriteria criteria, string language)
        {
            var result = new HandHistoryModel();
            result.ActionInfo = new ResultDetail();
            try
            {
                var voidbridgeApi = new VoidbridgeApiImp();
                int siteId = 3200;
                switch (criteria.RoleId)
                {
                    case 3:
                        siteId = 3100;
                        break;

                    case 2:
                        siteId = 3000;
                        break;

                    default:
                        break;
                }

                result.Iframe = voidbridgeApi.GetBetHistoryUrl(criteria.CustId.ToString(), criteria.GameplayId, language, siteId.ToString());
            }
            catch (Exception ex)
            {
                result.ActionInfo = new ResultDetail()
                {
                    ResultCode = 1,
                    ResultMessage = ex.Message
                };
            }

            return result;
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
            var rawData = _rngCasinoRepository.JackpotDetails(criteria);
            var detailsTickets = new List<TicketDetail>();

            if (rawData != null)
            {
                ITicketHelper ticketHelper = new TicketHelper();
                BetListHTMLFactory factory = new BetListHTMLFactory();
                BetListHTMLBuilder builder = factory.CreateBuilder();
                BetListHelper betListHelper = new BetListHelper();
                GetCachePropertyById funcGetOddsTypeName = ticketHelper.GetOddsTypeNameById;

                List<ITicketData> outputLiveCasino = new List<ITicketData>();
                foreach (var ticket in rawData)
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
                    detailsTickets.Add(ticketDetail);
                }
            }

            return detailsTickets;
        }
    }
}
