namespace Fanex.CustomerSetting.Module.SearchCopy.Repositories
{
    using System;
    using Data;
    using Entities;
    using NAP.Framework.Data;

    internal class NG2Repository : ING2Repository
    {
        public NG2Status GetStatus(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    if (reader.Read())
                    {
                        return new NG2Status(
                            Convert.ToBoolean(reader["issync"]),
                            Convert.ToBoolean(reader["isdisabled"]));
                    }
                }
            }

            return new NG2Status();
        }
    }
}