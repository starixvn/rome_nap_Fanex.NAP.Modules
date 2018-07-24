namespace Fanex.CustomerSetting.Module.SearchCopy.Repositories
{
    using System;
    using Core.Models;
    using Data;
    using NAP.Framework.Data;

    internal class DCSSettingCopyInsertMemberRepository : IDCSSettingCopyInsertMemberRepository
    {
        public Result CorrectMemberPositionTaking(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                var outputParams = db.GetOuputParams();

                return new Result(
                    Convert.ToInt32(outputParams["errcode"]),
                    outputParams["errmsg"].ToString());
            }
        }
    }
}