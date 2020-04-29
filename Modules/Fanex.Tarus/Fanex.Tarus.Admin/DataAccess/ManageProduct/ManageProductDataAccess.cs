using System.Collections.Generic;
using Fanex.Data;
using Fanex.Tarus.Admin.Criteria.ManageProduct;
using Fanex.Tarus.Admin.Entities;
using Fanex.Tarus.Admin.Entities.ManageProduct;
using Fanex.Tarus.Admin.Models.ManageProduct;
using Fanex.Tarus.Infrastructure.Helper;

namespace Fanex.Tarus.Admin.DataAccess.ManageProduct
{
    public interface IManageProductDataAccess
    {
        ProductModel Select(SelectProductCriteria criteria);

        Result Insert(InsertProductCriteria criteria);

        Result Update(UpdateProductCriteria criteria);
    }

    public class ManageProductDataAccess : BaseDataAccess, IManageProductDataAccess
    {
        public Result Insert(InsertProductCriteria criteria) => base.ExecuteNonQuery(criteria);

        public ProductModel Select(SelectProductCriteria criteria)
        {
            var entities = new List<ProductEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new ProductEntity
                    {
                        ProductID = DataUtils.ToString(reader["ProductID"]),
                        ProductName = DataUtils.ToString(reader["ProductName"]),
                        Description = DataUtils.ToString(reader["Description"]),
                        ProductPrefix = DataUtils.ToString(reader["ProductPrefix"]),
                        Enabled = DataUtils.ToString(reader["Enabled"]),
                        EndOfSupportDate = DataUtils.ToString(reader["EndOfSupportDate"]),
                        BalanceType = DataUtils.ToString(reader["ProductPrefix"]),
                        SubProductBalanceType = DataUtils.ToString(reader["SubProductBalanceType"]),
                        GroupType = DataUtils.ToString(reader["GroupType"]),
                        CasinoType = DataUtils.ToString(reader["CasinoType"]),
                        IsOfferPlayForFun = DataUtils.ToString(reader["IsOfferPlayForFun"]),
                        VendorID = DataUtils.ToString(reader["VendorID"]),
                        OrderNo = DataUtils.ToString(reader["OrderNo"])
                    });
                }

                reader.Close();
                return new ProductModel
                {
                    ProductEntities = entities,
                    TotalRows = DataUtils.ToInt32(objDb.GetOuputParams()["totalrows"])
                };
            }
        }

        public Result Update(UpdateProductCriteria criteria) => base.ExecuteNonQuery(criteria);
    }
}