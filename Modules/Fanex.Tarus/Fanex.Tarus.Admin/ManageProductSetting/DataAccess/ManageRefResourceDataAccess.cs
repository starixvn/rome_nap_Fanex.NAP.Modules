namespace Fanex.Tarus.Admin.ManageProductSetting.DataAccess
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Tarus.Admin.DataAccess;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageRefResource;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using Fanex.Tarus.Infrastructure.Helper;

    public interface IManageRefResourceDataAccess
    {
        RefResourceModel Select(SelectRefResourceCriteria criteria);

        Result Insert(InsertRefResourceCriteria criteria);

        Result Update(UpdateRefResourceCriteria criteria);

        Result Delete(DeleteRefResourceCriteria criteria);

    }
    public class ManageRefResourceDataAccess : BaseDataAccess, IManageRefResourceDataAccess
    {
        public Result Delete(DeleteRefResourceCriteria criteria)
            => ExecuteNonQuery(criteria);

        public Result Insert(InsertRefResourceCriteria criteria)
            => ExecuteNonQuery(criteria);

        public RefResourceModel Select(SelectRefResourceCriteria criteria)
        {
            var entities = new List<RefResourceEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new RefResourceEntity
                    {
                        RefId = DataUtils.ToString(reader["RefId"]),
                        ResourceId = DataUtils.ToString(reader["ResourceId"]),
                        ResourceEN = DataUtils.ToString(reader["Resource_en"]),
                        ResourceCH = DataUtils.ToString(reader["Resource_ch"]),
                        ResourceCS = DataUtils.ToString(reader["Resource_cs"]),
                        ResourceJP = DataUtils.ToString(reader["Resource_jp"]),
                        ResourceIT = DataUtils.ToString(reader["Resource_it"]),
                        ResourceTH = DataUtils.ToString(reader["Resource_th"]),
                        ResourceKO = DataUtils.ToString(reader["Resource_ko"]),
                        ResourceES = DataUtils.ToString(reader["Resource_es"]),
                        Sort1 = DataUtils.ToString(reader["Sort1"]),
                        Sort2 = DataUtils.ToString(reader["Sort2"]),
                        ResourceEL = DataUtils.ToString(reader["Resource_el"]),
                        ResourceVN = DataUtils.ToString(reader["Resource_vn"]),
                        ResourcePL = DataUtils.ToString(reader["Resource_pl"]),
                        ResourceID = DataUtils.ToString(reader["Resource_id"]),
                        ResourceHI = DataUtils.ToString(reader["Resource_hi"]),
                        ResourceMM = DataUtils.ToString(reader["Resource_mm"]),
                        ResourceFR = DataUtils.ToString(reader["Resource_fr"]),
                        ResourceRU = DataUtils.ToString(reader["Resource_ru"]),
                        ResourcePT = DataUtils.ToString(reader["Resource_pt"]),
                        ResourceDE = DataUtils.ToString(reader["Resource_de"]),
                        RowNum = DataUtils.ToInt32(reader["RowNum"])
                    });
                }

                reader.Close();
                return new RefResourceModel
                {
                    RefResources = entities,
                    TotalRows = DataUtils.ToInt32(objDb.GetOuputParams()["totalrows"])
                };
            }
        }

        public Result Update(UpdateRefResourceCriteria criteria)
            => ExecuteNonQuery(criteria);
    }
}
