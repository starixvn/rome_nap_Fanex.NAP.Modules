using System;
using Fanex.Data;
using Fanex.NAP.Framework.Data;
using Fanex.Tarus.Admin.DormantAccount.Criteria;
using Fanex.Tarus.Admin.DormantAccount.Models;
using Fanex.Tarus.Infrastructure.Helper;

namespace Fanex.Tarus.Admin.DormantAccount.DataAccess
{
    public interface IDormantAccountDataAccess
    {
        DormantAccountModel GetByUserNameMode(ICriteria criteria);

        DormantAccountModel GetByDateType(SelectDormantByDateTypeCriteria criteria);
    }

    public class DormantAccountDataAccess : IDormantAccountDataAccess
    {
        public DormantAccountModel GetByUserNameMode(ICriteria criteria)
        {
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                var model = new DormantAccountModel();
                while (reader.Read())
                {
                    model.Breadscrumb = new DormantBreadscrumbModel
                    {
                        CustId = DataUtils.ToInt32(reader["CustId"]),
                        DirectUsername = DataUtils.ToString(reader["DirectUsername"]),
                        DirectCustId = DataUtils.ToInt32(reader["DirectCustId"]),
                        HeadlineType = DataUtils.ToInt32(reader["HeadlineType"]),
                        CustHeadlineId = DataUtils.ToInt32(reader["CustHeadlineId"]),
                        HeadlineGroup = DataUtils.ToString(reader["HeadlineGroup"]),
                        SuperName = DataUtils.ToString(reader["SuperName"]),
                        MasterName = DataUtils.ToString(reader["MasterName"]),
                        AgentName = DataUtils.ToString(reader["AgentName"]),
                        Username = DataUtils.ToString(reader["Username"]),
                        RoleId = DataUtils.ToInt32(reader["RoleId"]),
                    };
                }

                reader.NextResult();
                while (reader.Read())
                {
                    model.ByUsername.Add(new DormantUsernameModel
                    {
                        CustId = DataUtils.ToInt32(reader["CustId"]),
                        Username = DataUtils.ToString(reader["Username"]),
                        RoleId = DataUtils.ToInt32(reader["RoleId"]),
                        Recommend = DataUtils.HasColumn(reader, "recommend") ? DataUtils.ToInt32(reader["recommend"]) :  0,
                        StatusId = DataUtils.ToInt32(reader["StatusId"]),
                        StageId = DataUtils.ToInt32(reader["StageId"]),
                        ToBeClearedGivenCredit = reader["ToBeClearedGivenCredit"] == DBNull.Value ? (Decimal?)null : DataUtils.ToDecimal(reader["ToBeClearedGivenCredit"]),
                        ClearedGivenCredit = reader["ClearedGivenCredit"] == DBNull.Value ? (Decimal?)null : DataUtils.ToDecimal(reader["ClearedGivenCredit"]),
                        LastLogged = reader["LastLogged"] == DBNull.Value ? (DateTime?)null : DataUtils.ToDateTime(reader["LastLogged"]),
                        ClosedDate = reader["ClosedDate"] == DBNull.Value ? (DateTime?)null : DataUtils.ToDateTime(reader["ClosedDate"]),
                        DisabledDate = reader["DisabledDate"] == DBNull.Value ? (DateTime?)null : DataUtils.ToDateTime(reader["DisabledDate"]),
                        SettingDeletedDate = reader["SettingDeletedDate"] == DBNull.Value ? (DateTime?)null : DataUtils.ToDateTime(reader["SettingDeletedDate"]),
                        IsWarningTicket = DataUtils.ToBoolean(reader["IsWarningTicket"]),
                        RowNumber = DataUtils.ToInt32(reader["RowNumber"]),
                        AccountDeletedDate = reader["AccountDeletedDate"] == DBNull.Value ? (DateTime?)null : DataUtils.ToDateTime(reader["AccountDeletedDate"]),
                    });
                }

                reader.NextResult();
                while (reader.Read())
                {
                    model.Balance.Add(new DormantBalanceModel
                    {
                        CustId = DataUtils.ToInt32(reader["CustId"]),
                        YesterdayTotalBalance = DataUtils.ToDecimal(reader["YesterdayTotalBalance"]),
                    });
                }
                reader.Close();
                model.TotalItem = DataUtils.ToInt32(objDb.GetOuputParams()["totalitems"]);

                return model;
            }
        }

        public DormantAccountModel GetByDateType(SelectDormantByDateTypeCriteria criteria)
        {
            var model = new DormantAccountModel();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        var byDateType = new DormantDateTypeModel
                        {
                            CustHeadlineId = DataUtils.ToInt32(reader["CustHeadlineId"]),
                            HeadlineGroup = DataUtils.ToString(reader["HeadlineGroup"]),
                            HeadlineType = reader["HeadlineType"] == DBNull.Value ? (Int32?)null : DataUtils.ToInt32(reader["HeadlineType"]),
                            CustId = DataUtils.ToInt32(reader["CustId"]),
                            Username = DataUtils.ToString(reader["Username"]),
                            RoleId = DataUtils.ToInt32(reader["RoleId"]),
                            StatusId = DataUtils.ToInt32(reader["StatusId"]),
                            StageId = DataUtils.ToInt32(reader["StageId"]),
                            ToBeClearedGivenCredit = reader["ToBeClearedGivenCredit"] == DBNull.Value ? (Decimal?)null : DataUtils.ToDecimal(reader["ToBeClearedGivenCredit"]),
                            ClearedGivenCredit = reader["ClearedGivenCredit"] == DBNull.Value ? (Decimal?)null : DataUtils.ToDecimal(reader["ClearedGivenCredit"]),
                            LastLogged = reader["LastLogged"] == DBNull.Value ? (DateTime?)null : DataUtils.ToDateTime(reader["LastLogged"]),
                            ClosedDate = reader["ClosedDate"] == DBNull.Value ? (DateTime?)null : DataUtils.ToDateTime(reader["ClosedDate"]),
                            DisabledDate = reader["DisabledDate"] == DBNull.Value ? (DateTime?)null : DataUtils.ToDateTime(reader["DisabledDate"]),
                            SettingDeletedDate = reader["SettingDeletedDate"] == DBNull.Value ? (DateTime?)null : DataUtils.ToDateTime(reader["SettingDeletedDate"]),
                            RowNumber = DataUtils.ToInt32(reader["RowNumber"]),
                            AccountDeletedDate = reader["AccountDeletedDate"] == DBNull.Value ? (DateTime?)null : DataUtils.ToDateTime(reader["AccountDeletedDate"])
                        };
                        byDateType.ArchivedMember.Close = DataUtils.ToInt32(reader["Cnt_Stage1Member"]);
                        byDateType.ArchivedMember.Disable = DataUtils.ToInt32(reader["Cnt_Stage2Member"]);
                        byDateType.ArchivedMember.DelSetting = DataUtils.ToInt32(reader["Cnt_Stage3Member"]);
                        byDateType.ArchivedMember.DelAccount = DataUtils.ToInt32(reader["Cnt_Stage4Member"]);
                        byDateType.ArchivedAgent.Close = DataUtils.ToInt32(reader["Cnt_Stage1Agent"]);
                        byDateType.ArchivedAgent.Disable = DataUtils.ToInt32(reader["Cnt_Stage2Agent"]);
                        byDateType.ArchivedAgent.DelSetting = DataUtils.ToInt32(reader["Cnt_Stage3Agent"]);
                        byDateType.ArchivedAgent.DelAccount = DataUtils.ToInt32(reader["Cnt_Stage4Agent"]);
                        byDateType.ArchivedMaster.Close = DataUtils.ToInt32(reader["Cnt_Stage1Master"]);
                        byDateType.ArchivedMaster.Disable = DataUtils.ToInt32(reader["Cnt_Stage2Master"]);
                        byDateType.ArchivedMaster.DelSetting = DataUtils.ToInt32(reader["Cnt_Stage3Master"]);
                        byDateType.ArchivedMaster.DelAccount = DataUtils.ToInt32(reader["Cnt_Stage4Master"]);
                        model.ByDateType.Add(byDateType);
                    }

                    reader.NextResult();
                    while (reader.Read())
                    {
                        model.Balance.Add(new DormantBalanceModel
                        {
                            CustId = DataUtils.ToInt32(reader["CustId"]),
                            YesterdayTotalBalance = DataUtils.ToDecimal(reader["YesterdayTotalBalance"]),
                        });
                    }
                    reader.Close();
                }

                model.TotalItem = DataUtils.ToInt32(db.GetOuputParams()["totalitems"]);
            }

            return model;
        }
    }
}
