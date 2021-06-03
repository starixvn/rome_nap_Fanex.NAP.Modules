namespace Fanex.Tarus.Admin.Business.MoveBetSchedule
{
    using Criteria.MoveBetSchedule;
    using Entities;
    using Entities.MoveBetSchedule;
    using System.Collections.Generic;

    public interface IMoveBetScheduleService
    {
        IEnumerable<MoveBetDayViewModel> GetMoveBetSchedule(GetMoveBetScheduleCriteriaBase criteria);

        Result UpdateMoveBetSchedule(UpdateMoveBetScheduleCriteria criteria);
    }
}