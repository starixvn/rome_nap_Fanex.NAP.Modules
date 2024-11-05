namespace Fanex.Tarus.Admin.ManageProductSetting.DataAccess
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Tarus.Admin.DataAccess;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageResetWinning;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Infrastructure.Helper;

    public interface IManageDwLListDataAccess
    {
        IEnumerable<DwLListEntity> Select(SelectDwLListCriteria criteria);

        Result Insert(InsertDwLListCriteria criteria);

        Result Update(UpdateDwLListCriteria criteria);

        Result Delete(DeleteDwLListCriteria criteria);
    }

    public class ManageDwLListDataAccess : BaseDataAccess, IManageDwLListDataAccess
    {
        public Result Delete(DeleteDwLListCriteria criteria)
            => ExecuteNonQuery(criteria);

        public Result Insert(InsertDwLListCriteria criteria)
            => ExecuteNonQuery(criteria);

        public IEnumerable<DwLListEntity> Select(SelectDwLListCriteria criteria)
        {
            var entities = new List<DwLListEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new DwLListEntity
                    {
                        CustId = DataUtils.ToString(reader["CustId"]),
                        Username = DataUtils.ToString(reader["Username"]),
                        Seq = DataUtils.ToString(reader["Seq"]),
                        Product = DataUtils.ToString(reader["Product"]),
                        ProductId = DataUtils.ToString(reader["ProductId"]),
                        IsDisabled = DataUtils.ToString(reader["IsDisabled"])
                    });
                }

                return entities;
            }
        }

        public Result Update(UpdateDwLListCriteria criteria)
            => ExecuteNonQuery(criteria);
    }
}
