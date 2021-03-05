namespace Fanex.Tarus.Admin.DataAccess.MoveBetSchedule
{
    using Common.Extensions;
    using Criteria.MoveBetSchedule;
    using Data;
    using Entities.MoveBetSchedule;
    using System.Collections.Generic;
    using System.Data;

    public class MoveBetScheduleDataAccess : IMoveBetScheduleDataAccess
    {
        public IEnumerable<MoveBetDay> GetMoveBetSchedule(GetMoveBetScheduleCriteriaBase criteria)
        {
            if (criteria.IsValid())
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                    {
                        while (reader.Read())
                        {
                            yield return BuildMoveBetDay(reader, criteria.IsDefault);
                        }
                    }
                }
            }
        }

        private static MoveBetDay BuildMoveBetDay(IDataReader reader, bool isDefaut)
            => new MoveBetDay
            {
                Date = isDefaut ? string.Empty : reader.ToString("scheduleDate"),
                WeekDay = isDefaut ? reader.ToInt("_weekDay") - 1 : reader.ToInt("weekday_") - 1,
                Options = new bool[] {
                    reader.ToBool("Isbackday1"),
                    reader.ToBool("Isbackday2"),
                    reader.ToBool("Isbackday3"),
                    reader.ToBool("Isbackday4"),
                    reader.ToBool("Isbackday5"),
                    reader.ToBool("Isbackday6"),
                    reader.ToBool("Isbackday7")
                }
            };
    }
}