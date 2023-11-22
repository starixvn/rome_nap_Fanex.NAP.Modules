namespace Fanex.Module.RNGCasino.Repositories
{
    using System;
    using System.Collections.Generic;
    using Criteria;
    using Fanex.Module.RNGCasino.Models;

    [Obsolete]
    public interface IRNGCasinoRepository
    {
        IEnumerable<TicketDto> BetDetails(GetRNGCasinoBetDetailsCriteria criteria);

        IEnumerable<TicketDto> BetDetails(GetRNGCasinoBetDetailsInSiteDBCriteria criteria, string AGIP);

        IEnumerable<TicketDto> JackpotDetails(GetJackpotDetailsCriteria criteria);

        IEnumerable<TicketDto> JackpotDetails(GetJackpotDetailsInSiteDBCriteria criteria, string AGIP);
    }
}