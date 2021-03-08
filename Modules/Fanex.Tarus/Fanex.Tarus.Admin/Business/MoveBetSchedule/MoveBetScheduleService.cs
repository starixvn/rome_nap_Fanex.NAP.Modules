namespace Fanex.Tarus.Admin.Business.MoveBetSchedule
{
    using DataAccess.MoveBetSchedule;
    using Fanex.Tarus.Admin.Criteria.MoveBetSchedule;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.MoveBetSchedule;
    using System.Collections.Generic;

    public class MoveBetScheduleService : IMoveBetScheduleService
    {
        private readonly IMoveBetScheduleDataAccess moveBetScheduleDataAccess;

        public MoveBetScheduleService(IMoveBetScheduleDataAccess moveBetScheduleDataAccess = null)
        {
            this.moveBetScheduleDataAccess = moveBetScheduleDataAccess ?? new MoveBetScheduleDataAccess();
        }

        public IEnumerable<MoveBetDay> GetMoveBetSchedule(GetMoveBetScheduleCriteriaBase criteria)
            => moveBetScheduleDataAccess.GetMoveBetSchedule(criteria);

        public Result UpdateMoveBetSchedule(UpdateMoveBetScheduleCriteria criteria)
            => moveBetScheduleDataAccess.UpdateMoveBetSchedule(criteria);
    }
}