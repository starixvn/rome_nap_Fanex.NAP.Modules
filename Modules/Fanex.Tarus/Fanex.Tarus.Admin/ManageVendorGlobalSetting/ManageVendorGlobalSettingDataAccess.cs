namespace Fanex.Tarus.Admin.ManageVendorGlobalSetting
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Tarus.Admin.DataAccess;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.SMAManagement;
    using Fanex.Tarus.Admin.ManageVendorGlobalSetting.Criteria;
    using Fanex.Tarus.Admin.ManageVendorGlobalSetting.Models;
    using Fanex.Tarus.Infrastructure.Helper;

    public interface IManageVendorGlobalSettingDataAccess
    {
        VendorGlobalModel Select(SelectVendorGlobalCriteria criteria);

        Result Insert(InsertVendorGlobalCriteria criteria);

        Result Update(UpdateVendorGlobalCriteria criteria);

        Result Delete(DeleteVendorGlobalCriteria criteria);
    }
    public class ManageVendorGlobalSettingDataAccess : BaseDataAccess, IManageVendorGlobalSettingDataAccess
    {
        public VendorGlobalModel Select(SelectVendorGlobalCriteria criteria)
        {
            var entities = new List<VenderGlobalEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new VenderGlobalEntity
                    {
                        RowNum = DataUtils.ToString(reader["rownum"]),
                        VendorId = DataUtils.ToString(reader["vendorid"]),
                        VendorName = DataUtils.ToString(reader["vname"]),
                        NumberValue = DataUtils.ToString(reader["numbervalue"]),
                        BetType = DataUtils.ToString(reader["bettype"]),
                        BetTypeName = DataUtils.ToString(reader["typenamee"]),
                        SportType = DataUtils.ToString(reader["sporttype"]),
                        SportTypeName = DataUtils.ToString(reader["sportname"])
                    });
                }

                reader.Close();
                return new VendorGlobalModel
                {
                    VendorGlobalEntities = entities,
                    TotalRows = DataUtils.ToInt32(objDb.GetOuputParams()["totalrows"])
                };
            }
        }

        public Result Insert(InsertVendorGlobalCriteria criteria) => ExecuteNonQuery(criteria);

        public Result Update(UpdateVendorGlobalCriteria criteria) => ExecuteNonQuery(criteria);

        public Result Delete(DeleteVendorGlobalCriteria criteria) => ExecuteNonQuery(criteria);
    }
}
