using Fanex.Data;
using Fanex.Tarus.Infrastructure.Caching;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Tarus.Infrastructure.Extensions
{
    public class SiteDB
    {
        public int SiteDBGroupID { get; set; }

        public string LinkedServerName { get; set; }

        public string ClientAccessName { get; set; }

        public string AGIP { get; set; }

        public bool IsSiteDB => SiteDBGroupID != 0;
    }

    public static class ObjectDbExtensions
    {
        private static string cacheSiteDBKey = "SiteDBs";

        private const int cacheSiteDBExpireTimeInHours = 1;

        public static IDataReader ExecuteReaderScaleoutDB(this ObjectDb objdb, Hashtable parameters)
            => objdb.ExecuteReaderScaleoutDB(parameters, (int)parameters["custid"]);

        public static IDataReader ExecuteReaderScaleoutDB(this ObjectDb objdb, Hashtable parameters, int custId)
        {
            string oldConnectionString = objdb.DbSetting.ConnectionString;
            var siteConfigs = GetSiteDBConfigByCustId(custId);
            if (siteConfigs != null && siteConfigs.IsSiteDB)
            {
                objdb.DbSetting.ConnectionString = BuildConnectionString(siteConfigs.AGIP, oldConnectionString);
            }

            objdb.AssignParamValues(parameters);
            IDataReader result = objdb.ExecuteReader();
            objdb.DbSetting.ConnectionString = oldConnectionString;
            return result;
        }

        public static IDataReader ExecuteReaderScaleoutDB(this ObjectDb objdb, Hashtable parameters, string AGIP)
        {
            string oldConnectionString = objdb.DbSetting.ConnectionString;
            objdb.DbSetting.ConnectionString = BuildConnectionString(AGIP, oldConnectionString);
            objdb.AssignParamValues(parameters);
            IDataReader result = objdb.ExecuteReader();
            objdb.DbSetting.ConnectionString = oldConnectionString;
            return result;
        }

        public static DataSet ExecuteDataSetScaleoutDB(this ObjectDb objdb, Hashtable parameters)
            => objdb.ExecuteDataSetScaleoutDB(parameters, (int)parameters["custid"]);

        public static DataSet ExecuteDataSetScaleoutDB(this ObjectDb objdb, Hashtable parameters, int custId)
        {
            string oldConnectionString = objdb.DbSetting.ConnectionString;
            var siteConfigs = GetSiteDBConfigByCustId(custId);
            if (siteConfigs != null && siteConfigs.IsSiteDB)
            {
                objdb.DbSetting.ConnectionString = BuildConnectionString(siteConfigs.AGIP, oldConnectionString);
            }

            objdb.AssignParamValues(parameters);
            DataSet result = objdb.ExecuteDataSet();
            objdb.DbSetting.ConnectionString = oldConnectionString;
            return result;
        }

        public static DataSet ExecuteDataSetScaleoutDB(this ObjectDb objdb, Hashtable parameters, string AGIP)
        {
            string oldConnectionString = objdb.DbSetting.ConnectionString;
            objdb.DbSetting.ConnectionString = BuildConnectionString(AGIP, oldConnectionString);
            objdb.AssignParamValues(parameters);
            DataSet result = objdb.ExecuteDataSet();
            objdb.DbSetting.ConnectionString = oldConnectionString;
            return result;
        }

        public static int ExecuteNonQueryScaleoutDB(this ObjectDb objdb, Hashtable parameters)
            => objdb.ExecuteNonQueryScaleoutDB(parameters, (int)parameters["custid"]);

        public static int ExecuteNonQueryScaleoutDB(this ObjectDb objdb, Hashtable parameters, int custId)
        {
            string oldConnectionString = objdb.DbSetting.ConnectionString;
            var siteConfigs = GetSiteDBConfigByCustId(custId);
            if (siteConfigs != null && siteConfigs.IsSiteDB)
            {
                objdb.DbSetting.ConnectionString = BuildConnectionString(siteConfigs.AGIP, oldConnectionString);
            }

            objdb.AssignParamValues(parameters);
            int result = objdb.ExecuteNonQuery();
            objdb.DbSetting.ConnectionString = oldConnectionString;
            return result;
        }

        public static int ExecuteNonQueryScaleoutDB(this ObjectDb objdb, Hashtable parameters, string AGIP)
        {
            string oldConnectionString = objdb.DbSetting.ConnectionString;
            objdb.DbSetting.ConnectionString = BuildConnectionString(AGIP, oldConnectionString);
            objdb.AssignParamValues(parameters);
            int result = objdb.ExecuteNonQuery();
            objdb.DbSetting.ConnectionString = oldConnectionString;
            return result;
        }

        public static SiteDB GetSiteDBConfigByCustId(int custId)
            => GetSiteDBConfig(new Hashtable { { "custid", custId } }).FirstOrDefault();

        public static SiteDB GetSiteDBConfigByUsername(string userName)
            => GetSiteDBConfig(new Hashtable { { "username", userName } }).FirstOrDefault();

        public static IEnumerable<SiteDB> GetSiteDBConfigBySiteGroupId(int siteGroupId)
            => GetSiteDBConfig(new Hashtable { { "sitedbgroupid", siteGroupId } });

        public static IEnumerable<SiteDB> GetAllSiteDBConfig()
            => GetSiteDBConfig(null);

        private static List<SiteDB> GetSiteDBConfig(Hashtable param)
        {
            List<SiteDB> siteDBs = null;
            if (IsGetAllSiteDBs(param))
            {
                siteDBs = CacheHelper.Get<List<SiteDB>>(cacheSiteDBKey);
            }

            if (siteDBs == null)
            {
                siteDBs = new List<SiteDB>();
                using (ObjectDb db = new ObjectDb("Acc_SiteDBConfigSel", 600))
                {
                    var reader = db.ExecuteReader(param);
                    while (reader.Read())
                    {
                        siteDBs.Add(new SiteDB()
                        {
                            SiteDBGroupID = Convert.ToInt32(reader["SiteDBGroupID"]),
                            AGIP = reader["AGIP"].ToString(),
                            ClientAccessName = reader["ClientAccessName"].ToString(),
                            LinkedServerName = reader["LinkedServerName"].ToString()
                        });
                    }
                }
                if (IsGetAllSiteDBs(param))
                {
                    CacheHelper.Set(cacheSiteDBKey, siteDBs, DateTime.Now.AddHours(cacheSiteDBExpireTimeInHours));
                }
            }

            return siteDBs;
        }

        private static string BuildConnectionString(string serverIP, string connectionString)
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            sqlConnectionStringBuilder.DataSource = serverIP;
            return sqlConnectionStringBuilder.ToString();
        }

        private static bool IsGetAllSiteDBs(Hashtable param)
        {
            if (param == null)
            {
                return true;
            }

            return false;
        }
    }
}
