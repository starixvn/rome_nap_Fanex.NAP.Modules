using Fanex.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Sunplus.Accounting.Core.Extensions
{
    public class SiteDB
    {
        public int SiteDBGroupID { get; set; }

        public string LinkedServerName { get; set; }

        public string ClientAccessName { get; set; }

        public string AGIP { get; set; }
    }

    public static class ObjectDbExtensions
    {
        public static int ExecuteNonQuery2(this ObjectDb objdb)
        {
            string oldConnectionString = objdb.DbSetting.ConnectionString;
            var siteConfigs = GetSiteDBConfigByCustId((int)objdb.GetParameterValue("custid")).FirstOrDefault();
            if (siteConfigs != null && !IsCreditSite(siteConfigs.SiteDBGroupID))
            {
                objdb.DbSetting.ConnectionString = BuildConnectionString(siteConfigs.AGIP, oldConnectionString);
            }

            int result = objdb.ExecuteNonQuery();
            objdb.DbSetting.ConnectionString = oldConnectionString;
            return result;
        }

        public static int ExecuteNonQuery2(this ObjectDb objdb, string AGIP)
        {
            string oldConnectionString = objdb.DbSetting.ConnectionString;
            objdb.DbSetting.ConnectionString = BuildConnectionString(AGIP, oldConnectionString);
            int result = objdb.ExecuteNonQuery();
            objdb.DbSetting.ConnectionString = oldConnectionString;
            return result;
        }

        public static IEnumerable<SiteDB> GetSiteDBConfigByCustId(int custId)
            => GetSiteDBConfig(new Hashtable { { "custid", custId } });

        public static IEnumerable<SiteDB> GetSiteDBConfigBySiteGroupId(int siteGroupId)
            => GetSiteDBConfig(new Hashtable { { "sitegroupid", siteGroupId } });

        private static bool IsCreditSite(int siteGroupId) => siteGroupId == 0;

        private static IEnumerable<SiteDB> GetSiteDBConfig(Hashtable param)
        {
            using (ObjectDb db = new ObjectDb("Acc_SiteDBConfigSel", 600))
            {
                var reader = db.ExecuteReader(param);
                while (reader.Read())
                {
                    yield return new SiteDB()
                    {
                        SiteDBGroupID = Convert.ToInt32(reader["SiteDBGroupID"]),
                        AGIP = reader["AGIP"].ToString(),
                        ClientAccessName = reader["ClientAccessName"].ToString(),
                        LinkedServerName = reader["LinkedServerName"].ToString()
                    };
                }
            }
        }

        private static string BuildConnectionString(string serverIP, string connectionString)
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            sqlConnectionStringBuilder.DataSource = serverIP;
            return sqlConnectionStringBuilder.ToString();
        }
    }
}
