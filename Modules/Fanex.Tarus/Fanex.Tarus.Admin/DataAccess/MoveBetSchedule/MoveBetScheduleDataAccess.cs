namespace Fanex.Tarus.Admin.DataAccess.MoveBetSchedule
{
    using Common.Extensions;
    using Criteria.MoveBetSchedule;
    using Data;
    using Entities.MoveBetSchedule;
    using Fanex.Tarus.Admin.Entities;
    using System;
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

        public Result UpdateMoveBetSchedule(UpdateMoveBetScheduleCriteria criteria)
        {
            if (criteria.IsValid())
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    db.ExecuteNonQuery(criteria.GetSpParams());

                    var outputParams = db.GetOuputParams();
                    if (outputParams != null)
                    {
                        return new Result(
                            (int)outputParams["errcode"],
                            outputParams["errmsg"].ToString());
                    }
                }
            }

            return null;
        }

        private static MoveBetDay BuildMoveBetDay(IDataReader reader, bool isDefault)
            => new MoveBetDay(
                isDefault ? DateTime.MinValue : reader.ToDateTime("scheduledate"),
                isDefault ? reader.ToInt("_weekday") : reader.ToInt("weekday_"),
                reader.ToBool("isbackday1"),
                reader.ToBool("isbackday2"),
                reader.ToBool("isbackday3"),
                reader.ToBool("isbackday4"),
                reader.ToBool("isbackday5"),
                reader.ToBool("isbackday6"),
                reader.ToBool("isbackday7")
            );
    }
}