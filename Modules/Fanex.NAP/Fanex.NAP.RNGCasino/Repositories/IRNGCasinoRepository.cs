namespace Fanex.Module.RNGCasino.Repositories
{
    using System;
    using System.Collections.Generic;
    using Criteria;
    using Fanex.BetList.Core.Entities;

    [Obsolete]
    public interface IRNGCasinoRepository
    {
        IEnumerable<Ticket> BetDetails(GetRNGCasinoBetDetailsCriteria criteria);

        IEnumerable<Ticket> JackpotDetails(GetJackpotDetailsCriteria criteria);
    }
}