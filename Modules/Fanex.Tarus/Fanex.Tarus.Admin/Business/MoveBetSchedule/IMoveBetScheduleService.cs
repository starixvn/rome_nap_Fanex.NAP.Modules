namespace Fanex.Tarus.Admin.Business.MoveBetSchedule
{
    using Criteria.MoveBetSchedule;
    using Entities.MoveBetSchedule;
    using System.Collections.Generic;

    public interface IMoveBetScheduleService
    {
        IEnumerable<MoveBetDay> GetMoveBetSchedule(GetMoveBetScheduleCriteriaBase criteria);
    }
}