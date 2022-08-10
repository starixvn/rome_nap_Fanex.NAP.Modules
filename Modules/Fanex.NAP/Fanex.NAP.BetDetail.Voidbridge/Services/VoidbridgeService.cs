namespace Fanex.Voidbridge.BetDetail.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Criteria;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Helpers;
    using Models;
    using Repositories;
    using Sunplus.Accounting.Core.Extensions;

    public class VoidbridgeService : IVoidbridgeService
    {
        private readonly IVoidbridgeRepository _rngCasinoRepository;

        public VoidbridgeService(IVoidbridgeRepository rngCasinoRepository = null)
        {
            _rngCasinoRepository = rngCasinoRepository ?? new VoidbridgeRepository();
        }

        public IEnumerable<TicketDetail> BetDetails(GetVoidbridgeBetDetailsCriteria criteria)
        {
            var detailsTickets = new List<TicketDetail>();            
            var siteDB = ObjectDbExtensions.GetSiteDBConfigByCustId(criteria.CustId);
            if (siteDB != null)
            {
                var siteDBTicket = new List<Ticket>();
                if (siteDB.IsSiteDB)
                {
                    var siteDBCriteria = new GetVoidbridgeBetDetailsInSiteDBCriteria
                    {
                        CustId = criteria.CustId,
                        PageIndex = criteria.PageIndex,
                        PageSize = criteria.PageSize,
                        TransId = criteria.TransId,
                        TransType = criteria.TransType
                    };
                    siteDBTicket = _rngCasinoRepository.BetDetails(siteDBCriteria, siteDB.AGIP).ToList();
                }

                var mainDBTickets = _rngCasinoRepository.BetDetails(criteria);
                var tickets = siteDBTicket.Concat(mainDBTickets);
                if (criteria.TotalRows > 0)
                {
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

                        detailsTickets.Add(ticketDetail);
                    }
                }
            }

            return detailsTickets;
        }
    }
}
