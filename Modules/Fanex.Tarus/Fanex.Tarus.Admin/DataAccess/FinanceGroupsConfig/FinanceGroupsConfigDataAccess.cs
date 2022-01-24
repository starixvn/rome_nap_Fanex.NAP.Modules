namespace Fanex.Tarus.Admin.DataAccess.FinanceGroupsConfig
{
    using DNAide.Core;
    using Fanex.Data;
    using Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Entities.SMAManagement;
    using System.Collections;
    using System.Collections.Generic;

    public class FinanceGroupsConfigDataAccess : IFinanceGroupsConfigDataAccess
    {
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
                else
                {
                    return new Result(0, string.Empty);
                }
            }
        }

        public IEnumerable<IdNameEntity> GroupSel(GetGroupsCriteria criteria)
        {
            var groups = new List<IdNameEntity>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while(reader.Read())
                    {
                        groups.Add(new IdNameEntity
                        {
                            Id = CastAide.AsInt32(reader["GroupId"]),
                            Name = "Group " + reader["GroupId"].ToString()
                        });
                    }
                }
            }

            return groups;
        }

        public FinanceGroupSelectResult FinanceGroupSel(FinanceGroupSelCriteria critera)
        {
            var result = new FinanceGroupSelectResult();
            using (var db = new ObjectDb(critera.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(critera.GetSpParams()))
                {
                    if (critera.FinanceType == 0)
                    {
                        while (reader.Read())
                        {
                            result.FinanceCodes.Add(new FinanceCodeSelectModel
                            {
                                SuperName = reader["SuperName"].ToString(),
                                MasterName = reader["MasterName"].ToString(),
                                AgentName = reader["AgentName"].ToString(),
                                MemberName = reader["Username"].ToString(),
                                FinanceCode = reader["FinanceCode"].ToString(),
                                IsView = CastAide.AsByte(reader["IsView"]),
                                GroupId = reader["GroupId"].ToString(),
                                RoleId = CastAide.AsInt32(reader["RoleId"])
                            });
                        }
                    } 
                    else 
                    {
                        while (reader.Read())
                        {
                            result.FinanceUsers.Add(new FinanceUserSelectModel
                            {
                                CustId = CastAide.AsInt32(reader["CustId"]),
                                UserName = reader["UserName"].ToString(),
                                GroupId = reader["GroupId"].ToString() == "" ? "None" : reader["GroupId"].ToString()
                            });
                        }
                    }
                    reader.Close();

                    var outputParams = db.GetOuputParams();
                    if (outputParams != null)
                    {
                        result.TotalItems = CastAide.AsInt32(outputParams["nrows"]);
                    }
                }
            }

            return result;
        }

        public Result FinanceGroupUpd(FinanceGroupUpdCriteria criteria)
            => ExecuteNonQuery(criteria.GetSettingKey(), criteria.GetSpParams());

        public Result FinanceGroupIsViewUpd(FinanceGroupIsViewUpdCriteria criteria)
            => ExecuteNonQuery(criteria.GetSettingKey(), criteria.GetSpParams());

        public Result FinanceGroupDel(FinanceGroupDelCriteria criteria)
            => ExecuteNonQuery(criteria.GetSettingKey(), criteria.GetSpParams());

        public FinanceGroupValidateResult FinanceGroupInsValidation(FinanceGroupInsValidationCriteria critera)
        {
            var record = new FinanceGroupValidateResult();
            using (var db = new ObjectDb(critera.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(critera.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        record.FinanceUsers.Add(new FinanceUserValidateModel
                        {
                            CustId = CastAide.AsInt32(reader["CustId"]),
                            GroupId = reader["GroupId"].ToString() == "" ? "None" : reader["GroupId"].ToString(),
                            RoleId = CastAide.AsInt32(reader["RoleId"]),
                            UserName = reader["UserName"].ToString(),
                            Status = reader["Status"].ToString()
                        });
                    }

                    reader.NextResult();
                    while (reader.Read())
                    {
                        record.FinanceCodes.Add(new FinanceCodeValidateModel
                        {
                            CustId = CastAide.AsInt32(reader["CustId"]),
                            RoleId = CastAide.AsInt32(reader["RoleId"]),
                            UserName = reader["UserName"].ToString(),
                            Status = reader["Status"].ToString(),
                            IsView = 1
                        });
                    }
                    reader.Close();

                    var outputParams = db.GetOuputParams();
                    if (outputParams != null)
                    {
                        record.IsCreateGroup = CastAide.AsByte(outputParams["iscreategroup"]);
                        record.ErrorCode = CastAide.AsInt32(outputParams["errcode"]);
                        record.ErrorMessage = outputParams["errmsg"].ToString();
                    }
                }
            }

            return record;
        }

        public FinanceGroupInsertResult FinanceGroupIns(FinanceGroupInsCriteria critera)
        {
            var result = new FinanceGroupInsertResult();
            using (var db = new ObjectDb(critera.GetSettingKey()))
            {
                db.ExecuteNonQuery(critera.GetSpParams());
                var outputParams = db.GetOuputParams();

                if (outputParams != null)
                {
                    result.ErrorCode = CastAide.AsInt32(outputParams["errcode"]);
                    result.ErrorMessage = outputParams["errmsg"].ToString();
                    result.GroupId = CastAide.AsInt32(outputParams["groupid"]);
                    result.NCodes = CastAide.AsInt32(outputParams["ncodes"]);
                    result.NUsers = CastAide.AsInt32(outputParams["nusers"]);
                }
            }

            return result;
        }

        public FinanceUserValidateResult FinanceUserInsValidation(FinanceUserInsValidationCriteria critera)
        {
            var record = new FinanceUserValidateResult();
            using (var db = new ObjectDb(critera.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(critera.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        record.FinanceUsers.Add(new FinanceUserValidateModel
                        {
                            CustId = CastAide.AsInt32(reader["CustId"]),
                            GroupId = reader["GroupId"].ToString() == "" ? "None" : reader["GroupId"].ToString(),
                            RoleId = CastAide.AsInt32(reader["RoleId"]),
                            UserName = reader["UserName"].ToString(),
                            Status = reader["Status"].ToString()
                        });
                    }
                    reader.Close();

                    var outputParams = db.GetOuputParams();
                    if (outputParams != null)
                    {
                        record.IsAssignUsers = CastAide.AsByte(outputParams["isassignusers"]);
                        record.ErrorCode = CastAide.AsInt32(outputParams["errcode"]);
                        record.ErrorMessage = outputParams["errmsg"].ToString();
                    }
                }
            }

            return record;
        }

        public FinanceUserInsertResult FinanceUserIns(FinanceUserInsCriteria critera)
        {
            var result = new FinanceUserInsertResult();
            using (var db = new ObjectDb(critera.GetSettingKey()))
            {
                db.ExecuteNonQuery(critera.GetSpParams());
                var outputParams = db.GetOuputParams();

                if (outputParams != null)
                {
                    result.ErrorCode = CastAide.AsInt32(outputParams["errcode"]);
                    result.ErrorMessage = outputParams["errmsg"].ToString();
                    result.NRows = CastAide.AsInt32(outputParams["nrows"]);
                }
            }

            return result;
        }

        public FinanceCodeValidateResult FinanceCodeInsValidation(FinanceCodeInsValidationCriteria critera)
        {
            var record = new FinanceCodeValidateResult();
            using (var db = new ObjectDb(critera.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(critera.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        record.FinanceCodes.Add(new FinanceCodeValidateModel
                        {
                            CustId = CastAide.AsInt32(reader["CustId"]),
                            RoleId = CastAide.AsInt32(reader["RoleId"]),
                            UserName = reader["UserName"].ToString(),
                            Status = reader["Status"].ToString(),
                            IsView = 1
                        });
                    }
                    reader.Close();

                    var outputParams = db.GetOuputParams();
                    if (outputParams != null)
                    {
                        record.IsAssignCodes = CastAide.AsByte(outputParams["isaddcodes"]);
                        record.ErrorCode = CastAide.AsInt32(outputParams["errcode"]);
                        record.ErrorMessage = outputParams["errmsg"].ToString();
                    }
                }
            }

            return record;
        }

        public FinanceCodeInsertResult FinanceCodeIns(FinanceCodeInsCriteria critera)
        {
            var result = new FinanceCodeInsertResult();
            using (var db = new ObjectDb(critera.GetSettingKey()))
            {
                db.ExecuteNonQuery(critera.GetSpParams());
                var outputParams = db.GetOuputParams();

                if (outputParams != null)
                {
                    result.ErrorCode = CastAide.AsInt32(outputParams["errcode"]);
                    result.ErrorMessage = outputParams["errmsg"].ToString();
                    result.NRows = CastAide.AsInt32(outputParams["nrows"]);
                }
            }

            return result;
        }
    }
}
