namespace Fanex.Tarus.Admin.ManageProductSetting.DataAccess
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Tarus.Admin.DataAccess;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageBettype;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using Fanex.Tarus.Infrastructure.Helper;

    public interface IManageBettypeDataAccess
    {
        BettypeModel Select(SelectBettypeCriteria criteria);

        Result Insert(InsertBettypeCriteria criteria);

        Result Update(UpdateBettypeCriteria criteria);
    }

    public class ManageBettypeDataAccess : BaseDataAccess, IManageBettypeDataAccess
    {
        public BettypeModel Select(SelectBettypeCriteria criteria)
        {
            var entities = new List<BettypeEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new BettypeEntity
                    {
                        TypeId = DataUtils.ToString(reader["typeid"]),
                        TypeNamee = DataUtils.ToString(reader["typenamee"]),
                        TypeNamec = DataUtils.ToString(reader["typenamec"]),
                        TypeDesc = DataUtils.ToString(reader["typedesc"]),
                        TypeNamec_uni = DataUtils.ToString(reader["typenamec_uni"]),
                        TypeNamej = DataUtils.ToString(reader["typenamej"]),
                        TypeNamcn = DataUtils.ToString(reader["typenamcn"]),
                        TypeNamecn = DataUtils.ToString(reader["typenamecn"]),
                        TypeNameth = DataUtils.ToString(reader["typenameth"]),
                        TypeNameit = DataUtils.ToString(reader["typenameit"]),
                        TypeNameko = DataUtils.ToString(reader["typenameko"]),
                        TypeNameES = DataUtils.ToString(reader["typenameES"]),
                        TypeNameVN = DataUtils.ToString(reader["typenameVN"]),
                        TypeNameEL = DataUtils.ToString(reader["typenameEL"]),
                        TypeNamee_Eu = DataUtils.ToString(reader["typenamee_Eu"]),
                        TypeNameRU = DataUtils.ToString(reader["typenameRU"]),
                        TypeNameID = DataUtils.ToString(reader["typenameID"]),
                        TypeNamePT = DataUtils.ToString(reader["typenamePT"]),
                        TypeNameFR = DataUtils.ToString(reader["typenameFR"]),
                        ParentTypeid = DataUtils.ToString(reader["ParentTypeid"]),
                        TypeNameMMR = DataUtils.ToString(reader["typenameMMR"]),
                        TypeNameHI = DataUtils.ToString(reader["typenameHI"]),
                        DisplayedTypeID = DataUtils.ToString(reader["DisplayedTypeID"]),
                    });
                }

                reader.Close();
                return new BettypeModel
                {
                    BettypeEntities = entities,
                    TotalRows = DataUtils.ToInt32(objDb.GetOuputParams()["totalrows"])
                };
            }
        }

        public Result Insert(InsertBettypeCriteria criteria) => base.ExecuteNonQuery(criteria);

        public Result Update(UpdateBettypeCriteria criteria) => base.ExecuteNonQuery(criteria);
    }
}