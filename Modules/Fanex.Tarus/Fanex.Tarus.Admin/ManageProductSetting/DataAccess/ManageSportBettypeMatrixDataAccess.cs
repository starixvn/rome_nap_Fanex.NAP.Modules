namespace Fanex.Tarus.Admin.ManageProductSetting.DataAccess
{
    using System;
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Tarus.Admin.DataAccess;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeMatrix;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using Fanex.Tarus.Infrastructure.Helper;

    public interface IManageSportBettypeMatrixDataAccess
    {
        SportBettypeMatrixModel Select(SelectSportBettypeMatrixCriteria criteria);

        Result Insert(InsertSportBettypeMatrixCriteria criteria);

        Result Update(UpdateSportBettypeMatrixCriteria criteria);

        Result Delete(DeleteSportBettypeMatrixCriteria criteria);
    }

    public class ManageSportBettypeMatrixDataAccess : BaseDataAccess, IManageSportBettypeMatrixDataAccess
    {
        public Result Insert(InsertSportBettypeMatrixCriteria criteria) => base.ExecuteNonQuery(criteria);

        public SportBettypeMatrixModel Select(SelectSportBettypeMatrixCriteria criteria)
        {
            var entities = new List<SportBettypeMatrixEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new SportBettypeMatrixEntity
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
                        RefTable = DataUtils.ToString(reader["RefTable"])
                    });
                }

                reader.Close();
                return new SportBettypeMatrixModel
                {
                    SportBettypeMatrixEntities = entities,
                    TotalRows = DataUtils.ToInt32(objDb.GetOuputParams()["totalrows"])
                };
            }
        }

        public Result Update(UpdateSportBettypeMatrixCriteria criteria) => base.ExecuteNonQuery(criteria);

        public Result Delete(DeleteSportBettypeMatrixCriteria criteria) => base.ExecuteNonQuery(criteria);
    }
}