namespace Fanex.Module.RNGCasino.Services
{
    using System;
    using System.Collections.Generic;
    using Criteria;
    using Models;
    using Models.HandHistory;
    using NPOI.SS.UserModel;

    public interface IRNGCasinoService
    {
        [Obsolete]
        IEnumerable<TicketDetail> BetDetails(GetRNGCasinoBetDetailsCriteria criteria);

        HandHistoryModel GetHandHistory(GetRNGCasinoResultCriteria criteria);

        HandHistoryModel GetVoidbridgeResult(GetVoidbridgeResultCriteria criteria, string language);

        HandHistoryModel GetResultCriteria(GetResultCriteria criteria, string language);

        IEnumerable<TicketDetail> JackpotDetails(GetJackpotDetailsCriteria criteria);
    }
}