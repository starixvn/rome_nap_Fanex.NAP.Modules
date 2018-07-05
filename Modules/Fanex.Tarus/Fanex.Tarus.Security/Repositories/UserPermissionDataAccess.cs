namespace Fanex.Tarus.Security.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Data;
    using Models;
    using NAP.Framework.Data;

    internal sealed class UserPermissionDataAccess : IUserPermissionDataAccess
    {
        public IEnumerable<UserPermission> GetUserPermissions(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        yield return ParseUserPermission(reader);
                    }
                }
            }
        }

        private static UserPermission ParseUserPermission(IDataReader reader)
            => new UserPermission(
                Convert.ToInt32(reader["groupid"]),
                reader["filenames"].ToString(),
                reader["funcname"].ToString(),
                Convert.ToByte(reader["actual_policy"]));
    }
}
