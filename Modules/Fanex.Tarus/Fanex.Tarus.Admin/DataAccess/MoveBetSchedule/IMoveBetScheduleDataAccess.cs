namespace Fanex.Tarus.Admin.DataAccess.MoveBetSchedule
{
    using Criteria.MoveBetSchedule;
    using Entities.MoveBetSchedule;
    using System.Collections.Generic;

    public interface IMoveBetScheduleDataAccess
    {
        IEnumerable<MoveBetDay> GetMoveBetSchedule(GetMoveBetScheduleCriteriaBase criteria);
    }
}