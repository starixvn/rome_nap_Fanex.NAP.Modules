using Fanex.Data;
using Sunplus.Accounting.Core.Cache.Extension;
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

        public int CustDBGroupID { get; set; }

        public string LinkedServerName { get; set; }

        public string ClientAccessName { get; set; }

        public string AGIP { get; set; }

        public bool IsSiteDB => SiteDBGroupID != 0;
    }

    public static class ObjectDbExtensions
    {
        private static string cacheSiteDBKey = "SiteDBs";

        private const int cacheSiteDBExpireTimeInHours = 1;

        private const string DBCUSTPrefix = "DBCUST_";

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

            objdb.AssignParamValues(parameters); IDataReader result;
            try
            {
                result = objdb.ExecuteReader();
            }
            finally
            {
                objdb.DbSetting.ConnectionString = oldConnectionString;
            }

            return result;
        }

        public static IDataReader ExecuteReaderScaleoutDB(this ObjectDb objdb, Hashtable parameters, string AGIP)
        {
            string oldConnectionString = objdb.DbSetting.ConnectionString;
            objdb.DbSetting.ConnectionString = BuildConnectionString(AGIP, oldConnectionString);
            objdb.AssignParamValues(parameters);
            IDataReader result;
            try
            {
                result = objdb.ExecuteReader();
            }
            finally
            {
                objdb.DbSetting.ConnectionString = oldConnectionString;
            }

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
            DataSet result;
            try
            {
                result = objdb.ExecuteDataSet();
            }
            finally
            {
                objdb.DbSetting.ConnectionString = oldConnectionString;
            }

            return result;
        }

        public static DataSet ExecuteDataSetScaleoutDB(this ObjectDb objdb, Hashtable parameters, string AGIP)
        {
            string oldConnectionString = objdb.DbSetting.ConnectionString;
            objdb.DbSetting.ConnectionString = BuildConnectionString(AGIP, oldConnectionString);
            objdb.AssignParamValues(parameters);
            DataSet result;
            try
            {
                result = objdb.ExecuteDataSet();
            }
            finally
            {
                objdb.DbSetting.ConnectionString = oldConnectionString;
            }

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
            int result;
            try
            {
                result = objdb.ExecuteNonQuery();
            }
            finally
            {
                objdb.DbSetting.ConnectionString = oldConnectionString;
            }

            return result;
        }

        public static int ExecuteNonQueryScaleoutDB(this ObjectDb objdb, Hashtable parameters, string AGIP)
        {
            string oldConnectionString = objdb.DbSetting.ConnectionString;
            objdb.DbSetting.ConnectionString = BuildConnectionString(AGIP, oldConnectionString);
            objdb.AssignParamValues(parameters);
            int result;
            try
            {
                result = objdb.ExecuteNonQuery();
            }
            finally
            {
                objdb.DbSetting.ConnectionString = oldConnectionString;
            }

            return result;
        }

        public static SiteDB GetSiteDBConfigBySite(string site)
            => GetSiteDBConfig(new Hashtable { { "site", site } }, "Acc_ServerDBConfigSel").FirstOrDefault();

        public static SiteDB GetSiteDBConfigByCustId(int custId)
            => GetSiteDBConfig(new Hashtable { { "custid", custId } }, "Acc_ServerDBConfigSelByCust").FirstOrDefault();

        public static SiteDB GetSiteDBConfigByUsername(string userName)
            => GetSiteDBConfig(new Hashtable { { "username", userName } }, "Acc_ServerDBConfigSelByCust").FirstOrDefault();

        public static IEnumerable<SiteDB> GetAllSiteDBConfig()
            => GetSiteDBConfig(null, "Acc_ServerDBConfigSel");

        private static List<SiteDB> GetSiteDBConfig(Hashtable param, string spName)
        {
            List<SiteDB> siteDBs = null;
            if (IsGetAllSiteDBs(param))
            {
                siteDBs = CacheExtension.Get<List<SiteDB>>(cacheSiteDBKey);
            }

            if (siteDBs == null)
            {
                siteDBs = new List<SiteDB>();
                using (ObjectDb db = new ObjectDb(spName, 600))
                {
                    var reader = db.ExecuteReader(param);
                    while (reader.Read())
                    {
                        siteDBs.Add(new SiteDB()
                        {
                            SiteDBGroupID = Convert.ToInt32(reader["SiteDBGroupID"]),
                            CustDBGroupID = Convert.ToInt32(reader["CustDBGroupID"]),
                            AGIP = reader["AGIP"].ToString(),
                            ClientAccessName = reader["ClientAccessName"].ToString(),
                            LinkedServerName = reader["LinkedServerName"].ToString()
                        });
                    }
                }
                if (IsGetAllSiteDBs(param))
                {
                    CacheExtension.Set(cacheSiteDBKey, siteDBs, DateTime.Now.AddHours(cacheSiteDBExpireTimeInHours));
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
