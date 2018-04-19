namespace Fanex.Module.RNGCasino.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Module.RNGCasino.Models;

    public class JackpotDetailsViewModel
    {
        public IEnumerable<TicketDetail> TicketDetails { get; set; }

        public int TotalRows => TicketDetails.Count();
    }
}