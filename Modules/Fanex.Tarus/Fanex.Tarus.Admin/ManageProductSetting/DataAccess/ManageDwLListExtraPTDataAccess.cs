namespace Fanex.Tarus.Admin.ManageProductSetting.DataAccess
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Tarus.Admin.DataAccess;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageResetWinning;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Infrastructure.Helper;

    public interface IManageDwLListExtraPTDataAccess {


        IEnumerable<DwLListExtraPTEntity> Select(SelectDwLListExtraPTCriteria criteria);

        Result Insert(InsertDwLListExtraPTCriteria criteria);

        Result Update(UpdateDwLListExtraPTCriteria criteria);

        Result Delete(DeleteDwLListExtraPTCriteria criteria);
    }

    public class ManageDwLListExtraPTDataAccess : BaseDataAccess, IManageDwLListExtraPTDataAccess
    {
        public Result Delete(DeleteDwLListExtraPTCriteria criteria)
            => ExecuteNonQuery(criteria);

        public Result Insert(InsertDwLListExtraPTCriteria criteria)
            => ExecuteNonQuery(criteria);

        public IEnumerable<DwLListExtraPTEntity> Select(SelectDwLListExtraPTCriteria criteria)
        {
            var entities = new List<DwLListExtraPTEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new DwLListExtraPTEntity
                    {
                        CustId = DataUtils.ToString(reader["CustId"]),
                        Username = DataUtils.ToString(reader["Username"]),
                        GroupId = DataUtils.ToString(reader["GroupId"]),
                        GroupName = DataUtils.ToString(reader["GroupName"]),
                        IsDisabled = DataUtils.ToString(reader["IsDisabled"])
                    });
                }

                return entities;
            }
        }

        public Result Update(UpdateDwLListExtraPTCriteria criteria)
            => ExecuteNonQuery(criteria);
    }
}
