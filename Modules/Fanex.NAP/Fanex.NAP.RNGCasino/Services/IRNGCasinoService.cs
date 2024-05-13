namespace Fanex.Module.RNGCasino.Services
{
    using System;
    using System.Collections.Generic;
    using Criteria;
    using Models;

    public interface IRNGCasinoService
    {
        [Obsolete]
        IEnumerable<TicketDetail> BetDetails(GetRNGCasinoBetDetailsCriteria criteria);

        IEnumerable<TicketDetail> JackpotDetails(GetJackpotDetailsCriteria criteria);
    }
}
