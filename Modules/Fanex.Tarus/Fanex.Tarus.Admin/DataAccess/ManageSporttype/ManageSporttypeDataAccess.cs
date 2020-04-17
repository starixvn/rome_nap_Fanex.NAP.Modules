namespace Fanex.Tarus.Admin.DataAccess.ManageSporttype
{
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.Criteria.ManageSporttype;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.ManageSporttype;
    using Fanex.Tarus.Admin.Models.ManageSporttype;
    using Fanex.Tarus.Infrastructure.Helper;
    using System.Collections.Generic;

    public interface IManageSporttypeDataAccess
    {
        SportTypeModel Select(SelectSportTypeCriteria criteria);

        Result Insert(InsertSportTypeCriteria criteria);

        Result Update(UpdateSportTypeCriteria criteria);
    }

    public class ManageSporttypeDataAccess : BaseDataAccess, IManageSporttypeDataAccess
    {
        public SportTypeModel Select(SelectSportTypeCriteria criteria)
        {
            var entities = new List<SportTypeEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new SportTypeEntity
                    {
                        SportType = DataUtils.ToString(reader["SportType"]),
                        SportName = DataUtils.ToString(reader["SportName"]),
                        SportNameCh = DataUtils.ToString(reader["SportNameCh"]),
                        SportNameCs = DataUtils.ToString(reader["SportNameCs"]),
                        SportNameJp = DataUtils.ToString(reader["SportNameJp"]),
                        SportNameIt = DataUtils.ToString(reader["SportNameIt"]),
                        SportNameTh = DataUtils.ToString(reader["SportNameTh"]),
                        ParlayMaxPayout = DataUtils.ToString(reader["ParlayMaxPayout"]),
                        SportNameKo = DataUtils.ToString(reader["SportNameKo"]),
                        SportNameES = DataUtils.ToString(reader["SportNameES"]),
                        SportNameVN = DataUtils.ToString(reader["SportNameVN"]),
                        SportNameEL = DataUtils.ToString(reader["SportNameEL"]),
                        SportNameRU = DataUtils.ToString(reader["SportNameRU"]),
                        SportNameID = DataUtils.ToString(reader["SportNameID"]),
                        SportNamePT = DataUtils.ToString(reader["SportNamePT"]),
                        SportNameFR = DataUtils.ToString(reader["SportNameFR"]),
                        SportNameMMR = DataUtils.ToString(reader["SportNameMMR"]),
                        SportNamePL = DataUtils.ToString(reader["SportNamePL"]),
                        SportNameHI = DataUtils.ToString(reader["SportNameHI"])
                    });
                }

                reader.Close();
                return new SportTypeModel
                {
                    SportTypeEntities = entities,
                    TotalRows = DataUtils.ToInt32(objDb.GetOuputParams()["totalrows"])
                };
            }
        }

        public Result Insert(InsertSportTypeCriteria criteria) => base.ExecuteNonQuery(criteria);

        public Result Update(UpdateSportTypeCriteria criteria) => base.ExecuteNonQuery(criteria);
    }
}
