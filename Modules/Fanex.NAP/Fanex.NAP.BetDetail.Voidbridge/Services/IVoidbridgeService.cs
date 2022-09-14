namespace Fanex.Voidbridge.BetDetail.Services
{
    using System.Collections.Generic;
    using Criteria;
    using Models;

    public interface IVoidbridgeService
    {
        IEnumerable<TicketDetail> BetDetails(GetVoidbridgeBetDetailsCriteria criteria);
    }
}
