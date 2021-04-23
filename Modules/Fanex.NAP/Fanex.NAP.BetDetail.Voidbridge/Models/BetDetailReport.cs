using System.Collections.Generic;
using System.Linq;
using Fanex.BetList.Core.TicketHelper;

namespace Fanex.Voidbridge.BetDetail.Models
{
    public class BetDetailReport
    {
        public IEnumerable<TicketDetail> TicketDetails { get; set; }

        public int TotalRows { get; set; }

        public string ResultTile
        {
            get
            {
                if (TicketDetails.Any())
                {
                    var sportType = TicketDetails.First().Ticket.SportTypeId;
                    var bettype = TicketDetails.First().Ticket.BetTypeId;
                    var ticketHelper = new TicketHelper();

                    return $"{ticketHelper.GetSportNameById(sportType)} - {ticketHelper.GetBetTypeNameById(bettype)}";
                }

                return string.Empty;
            }
        }
    }
}
