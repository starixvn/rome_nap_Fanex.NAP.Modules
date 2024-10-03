using Fanex.Data;
using Fanex.Tarus.Admin.Criteria.SMAManagement;
using Fanex.Tarus.Admin.Entities;
using Fanex.Tarus.Admin.Entities.SMAManagement;
using Sunplus.Accounting.Core.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Sunplus.Accounting.DataAccess.IBCDLL.Accounting;

namespace Fanex.Tarus.Admin.DataAccess.SMAManagement
{
    public class SMAManagementAccess : ISMAManagementAccess
    {
        public IEnumerable<SiteEntity> GetSites(int mode)
        {
            IList<SiteEntity> sites = new List<SiteEntity>(32);
            using (var db = new ObjectDb("Acc_SiteSel_Credit"))
            {
                var inputParams = new Hashtable
                {
                    { "userid", AppSession.AccId },
                    {  "mode", mode }
                };

                using (var reader = db.ExecuteReader(inputParams))
                {
                    while (reader.Read())
                    {
                        sites.Add(new SiteEntity
                        {
                            SiteId = Convert.ToInt32(reader["siteid"]),
                            SiteName = reader["site"].ToString(),
                            SiteLabel = reader["sitelabel"].ToString()
                        });
                    }
                }
            }

            return sites;
        }

        public IEnumerable<IdNameEntity> GetCurrencies(bool excludeTestCurrencies)
        {
            IList<IdNameEntity> currencies = new List<IdNameEntity>(64);

            using (var db = new ObjectDb("Acc_ExchangeSel"))
            {
                var inputParams = new Hashtable
                {
                    { "istest", !excludeTestCurrencies },
                    { "userid", AppSession.AccId },
                    { "deniedlist" , AccountCredit.Acc_DeniedSiteCurrency_Sel(AppSession.AccId, 2)}
                };

                using (var reader = db.ExecuteReader(inputParams))
                {
                    while (reader.Read())
                    {
                        currencies.Add(new IdNameEntity
                        {
                            Id = Convert.ToInt32(reader["exchangeid"]),
                            Name = reader["currency"].ToString()
                        });
                    }
                }
            }

            return currencies;
        }

        public IEnumerable<SMAGroupHeadline> GetSMAGroupHeadlines(GetSMAGroupHeadlineCriteria criteria)
        {
            IDictionary<int, SMAGroupHeadline> groupHeadlinesHash = new Dictionary<int, SMAGroupHeadline>(128);

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSPParams()))
                {
                    SMAEntity entity;
                    SMAGroupHeadline groupHeadline;

                    while (reader.Read())
                    {
                        entity = ParseSMAEntity(reader);

                        if (!groupHeadlinesHash.TryGetValue(entity.GroupId, out groupHeadline))
                        {
                            groupHeadline = new SMAGroupHeadline
                            {
                                GroupId = entity.GroupId,
                                GroupName = entity.GroupName,
                                Owner = entity.Owner,
                                SMAEntities = new List<SMAEntity>(64)
                            };

                            groupHeadlinesHash.Add(entity.GroupId, groupHeadline);
                        }

                        groupHeadline.SMAEntities.Add(entity);
                    }
                }
            }

            return groupHeadlinesHash.Values;
        }

        public Result UpdateHeadlineType(UpdateHeadlineTypeCriteria criteria)
        {
            return ExecuteNonQuery(criteria.GetSettingKey(), criteria.GetSPParams());
        }

        public Result UpdateOwner(UpdateOwnerCriteria criteria)
        {
            return ExecuteNonQuery(criteria.GetSettingKey(), criteria.GetSPParams());
        }

        private Result ExecuteNonQuery(string settingKey, Hashtable inputParams)
        {
            using (var db = new ObjectDb(settingKey))
            {
                db.ExecuteNonQuery(inputParams);
                var outputParams = db.GetOuputParams();

                if (outputParams != null)
                {
                    return new Result((int)outputParams["errcode"], outputParams["errmsg"].ToString());
                }
            }

            return null;
        }

        private SMAEntity ParseSMAEntity(IDataReader reader)
        {
            var entity = new SMAEntity
            {
                GroupId = Convert.ToInt32(reader["custheadlineid"]),
                GroupName = reader["groupname"].ToString(),
                CustId = Convert.ToInt32(reader["custid"]),
                Username = reader["username"].ToString(),
                IsSuspend = Convert.ToBoolean(reader["issuspend"]),
                IsClosed = Convert.ToBoolean(reader["isclosed"]),
                IsEnable = Convert.ToBoolean(reader["isenable"]),
                Site = reader["site"].ToString(),
                Currency = reader["currencyname"].ToString(),
                RoleId = Convert.ToInt32(reader["roleid"]),
                Owner = reader["ownername"].ToString()
            };

            object headlineType = reader["headlinetype"];

            if (headlineType == null || headlineType is DBNull)
            {
                entity.HeadlineTypeId = null;
            }
            else
            {
                entity.HeadlineTypeId = Convert.ToInt32(headlineType);
            }

            if (entity.IsClosed)
            {
                entity.ClosedReason = new CustomerClosedReason(reader["closedreason"]);
            }

            return entity;
        }

        string getStatusName(int statusId)
        {
            switch (statusId)
            {
                case 0:
                    return "Disabled";
                case 1:
                    return "Inactive";
                case 2:
                    return "Suspended";
                case 3:
                    return "View Only";
                case 4:
                    return "Active";
                default:
                    return "";
            }

        }
    }
}
