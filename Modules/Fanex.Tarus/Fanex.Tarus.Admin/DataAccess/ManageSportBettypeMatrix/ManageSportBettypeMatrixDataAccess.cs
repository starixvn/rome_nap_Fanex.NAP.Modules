using System.Collections.Generic;
using Fanex.Data;
using Fanex.Tarus.Admin.Criteria.ManageSportBettypeMatrix;
using Fanex.Tarus.Admin.Entities;
using Fanex.Tarus.Admin.Entities.ManageSportBettypeMatrix;
using Fanex.Tarus.Admin.Models.ManageSportBettypeMatrix;
using Fanex.Tarus.Infrastructure.Helper;

namespace Fanex.Tarus.Admin.DataAccess.ManageSportBettypeMatrix
{
    public interface IManageSportBettypeMatrixDataAccess
    {
        SportBettypeMatrixModel Select(SelectSportBettypeMatrixCriteria criteria);

        Result Insert(InsertSportBettypeMatrixCriteria criteria);

        Result Update(UpdateSportBettypeMatrixCriteria criteria);
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
                        BetType = DataUtils.ToString(reader["BetType"]),
                        IsLive = DataUtils.ToBoolean(reader["IsLive"]),
                        ProductId = DataUtils.ToString(reader["ProductID"]),
                        Enabled = DataUtils.ToBoolean(reader["Enabled"]),
                        IsTest = DataUtils.ToBoolean(reader["IsTest"]),
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
    }
}