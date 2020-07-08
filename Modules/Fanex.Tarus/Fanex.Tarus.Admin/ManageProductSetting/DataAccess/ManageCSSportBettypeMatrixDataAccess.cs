namespace Fanex.Tarus.Admin.ManageProductSetting.DataAccess
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.DataAccess;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageCSSportBettypeMatrix;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using Fanex.Tarus.Infrastructure.Helper;

    public interface IManageCSSportBettypeMatrixDataAccess
    {
        CSSportBettypeMatrixModel Select(SelectCSSportBettypeMatrixCriteria criteria);

        Result Insert(InsertCSSportBettypeMatrixCriteria criteria);

        Result Update(ICriteria criteria);

        Result Delete(ICriteria criteria);
    }

    public class ManageCSSportBettypeMatrixDataAccess : BaseDataAccess, IManageCSSportBettypeMatrixDataAccess
    {
        public Result Insert(InsertCSSportBettypeMatrixCriteria criteria) => base.ExecuteNonQuery(criteria);

        public CSSportBettypeMatrixModel Select(SelectCSSportBettypeMatrixCriteria criteria)
        {
            var entities = new List<CSSportBettypeMatrixEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new CSSportBettypeMatrixEntity
                    {
                        SportType = DataUtils.ToString(reader["SportType"]),
                        SportTypeName = DataUtils.ToString(reader["SportName"]),
                        BetType = DataUtils.ToString(reader["BetType"]),
                        BetTypeName = DataUtils.ToString(reader["BettypeName"]),
                        IsLive = DataUtils.ToString(reader["IsLive"]),
                        ProductId = DataUtils.ToString(reader["ProductID"]),
                        ProductName = DataUtils.ToString(reader["ProductName"]),
                        Enabled = DataUtils.ToString(reader["Enabled"]),
                        IsTest = DataUtils.ToString(reader["IsTest"]),
                        EventType = DataUtils.ToString(reader["EventType"]),
                        SubProduct = DataUtils.ToString(reader["SubProduct"]),
                        RefTable = DataUtils.ToString(reader["RefTable"]),
                        BetTypeGroupId = DataUtils.ToString(reader["BetTypeGroup"]),
                        BetTypeGroupName = DataUtils.ToString(reader["BetTypeGroupName"]),
                        SportTypeGroupId = DataUtils.ToString(reader["SportTypeGroup"]),
                        SportTypeGroupName = DataUtils.ToString(reader["SportTypeGroupName"]),
                    });
                }

                reader.Close();
                return new CSSportBettypeMatrixModel
                {
                    CSSportBettypeMatrixEntities = entities,
                    TotalRows = DataUtils.ToInt32(objDb.GetOuputParams()["totalrows"])
                };
            }
        }

        public Result Update(ICriteria criteria) => base.ExecuteNonQuery(criteria);

        public Result Delete(ICriteria criteria) => base.ExecuteNonQuery(criteria);
    }
}
