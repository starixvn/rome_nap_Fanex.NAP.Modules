using System;
using System.Collections;
using System.Collections.Generic;
using DNAide.Core;
using Fanex.Data;
using Sunplus.Accounting.DataAccess.IAdmin;
using Sunplus.Accounting.Entities.Admin;
using Sunplus.Accounting.Entities;
using Fanex.CustomerSetting.Module.Core;
using Sunplus.Accounting.DataAccess.IBCDLL.Accounting;

namespace Sunplus.Accounting.DataAccess.AdoNet.Admin
{
    public class ExternalSiteDAO : DataAccess, IExternalSiteDAO
    {
        public Result InitSuperInfo(string superName)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerInsSuperInfo"))
            {
                var inputParams = new Hashtable { { "custname", superName } };
                db.AssignParamValues(inputParams);
                db.ExecuteNonQuery();

                return GetResultFromOutputHash(db.GetOuputParams());
            }
        }

        public IEnumerable<string> GetGroupNames()
        {
            IList<string> groups = new List<string>(16);

            using (ObjectDb db = new ObjectDb("SelectGroupName"))
            {
                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        groups.Add(reader["GroupName"].ToString());
                    }
                }
            }

            return groups;
        }

        public IEnumerable<ExternalSiteSuper> GetSuperList(string groupName, int userId)
        {
            var param = new Hashtable
            {
                { "groupname", groupName },
                { "deniedlist",   AccountCredit.Acc_DeniedSiteCurrency_Sel(userId) }
            };

            using (ObjectDb db = new ObjectDb("SelectInfoCust"))
            {
                return db.Query<ExternalSiteSuper>(param);
            }
        }

        public Result AddSuperToGroup(string username, string groupName, int userId)
        {
            var inputParams = new Hashtable
            {
                { "username", username },
                { "userid", userId },
                { "groupname", groupName }
            };

            using (ObjectDb db = new ObjectDb("AddExistCustomer"))
            {
                db.AssignParamValues(inputParams);
                db.ExecuteNonQuery();

                return GetResultFromOutputHash(db.GetOuputParams());
            }
        }

        public Result RemoveSuperFromGroup(int custId, string groupName, int userId)
        {
            var param = new Hashtable
            {
                { "custid", custId },
                { "groupname", groupName },
                { "userid", userId }
            };

            using (ObjectDb db = new ObjectDb("DeleteCustomer"))
            {
                db.AssignParamValues(param);
                db.ExecuteNonQuery();

                return GetResultFromOutputHash(db.GetOuputParams());
            }
        }

        public ExternalSiteExtraPTGroup GetExtraPTGroups(int custId)
        {
            var param = new Hashtable
            {
                { "custid", custId }
            };

            ExternalSiteExtraPTGroup result = new ExternalSiteExtraPTGroup();
            var entities = new List<ExternalSiteExtraPTGroupEntity>();
            using (ObjectDb db = new ObjectDb("Acc_CustGroupSelExtraPTGroupByCustID"))
            {
                var reader = db.ExecuteReader(param);
                while (reader.Read())
                {
                    entities.Add(new ExternalSiteExtraPTGroupEntity
                    {
                        EffectiveDate = Utils.ToDateTime(reader["EffectiveDate"]),
                        GroupName = Utils.ToString(reader["GroupName"]),
                        IsPrimary = Utils.ToBoolean(reader["IsPrimary"])
                    });
                }

                reader.Close();
                result.Entities = entities;
                result.NoOfMember = Utils.ToInt32(db.GetOuputParams()["noofmember"]);
            }

            return result;
        }

        public Result ChangePrimaryGroup(int custId, string groupName, DateTime effectiveDate, int userId, string ip)
        {
            var param = new Hashtable
            {
                { "custid", custId },
                { "groupname", groupName },
                { "effectivedate", effectiveDate },
                { "userid", userId },
                { "ip", ip }
            };

            using (ObjectDb db = new ObjectDb("Acc_CustGroupUpdPrimaryExtraPTGroup"))
            {
                db.AssignParamValues(param);
                db.ExecuteNonQuery();

                return GetResultFromOutputHash(db.GetOuputParams());
            }
        }

        private Result GetResultFromOutputHash(Hashtable outputParams)
        {
            return new Result
            {
                ErrorCode = CastAide.AsInt32(outputParams["errcode"]),
                ErrorMessage = CastAide.AsString(outputParams["errmsg"])
            };
        }
    }
}
