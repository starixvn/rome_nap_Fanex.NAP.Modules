namespace Fanex.Tarus.Admin.ManageProductSetting.DataAccess
{
    using System;
    using System.Collections.Generic;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Tarus.Admin.DataAccess;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageCSSportBettypeMatrix;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageProduct;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using Fanex.Tarus.Infrastructure.Helper;

    public interface IManageProductDataAccess
    {
        ProductModel Select(SelectProductCriteria criteria);

        Result Insert(InsertProductCriteria criteria);

        Result Update(UpdateProductCriteria criteria);
        IEnumerable<BetTypeGroupEntity> SelectBetTypeGroup(SelectBetTypeGroupCriteria selectBetTypeGroupCriteria);
        IEnumerable<SportTypeGroupEntity> SelectSportTypeGroup(SelectSportTypeGroupCriteria selectSportTypeGroupCriteria);
    }

    public class ManageProductDataAccess : BaseDataAccess, IManageProductDataAccess
    {
        public Result Insert(InsertProductCriteria criteria) => base.ExecuteNonQuery(criteria);

        public ProductModel Select(SelectProductCriteria criteria)
        {
            var entities = new List<ProductEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        entities.Add(new ProductEntity
                        {
                            ProductID = DataUtils.ToString(reader["ProductID"]),
                            ProductName = DataUtils.ToString(reader["ProductName"]),
                            Description = DataUtils.ToString(reader["Description"]),
                            ProductPrefix = DataUtils.ToString(reader["ProductPrefix"]),
                            Enabled = DataUtils.ToString(reader["Enabled"]),
                            EndOfSupportDate = reader["EndOfSupportDate"] == DBNull.Value ? null : DataUtils.ToDateTime(reader["EndOfSupportDate"]).ToShortDateString(),
                            BalanceType = DataUtils.ToString(reader["BalanceType"]),
                            SubProductBalanceType = DataUtils.ToString(reader["SubProductBalanceType"]),
                            GroupType = DataUtils.ToString(reader["GroupType"]),
                            CasinoType = DataUtils.ToString(reader["CasinoType"]),
                            IsOfferPlayForFun = DataUtils.ToString(reader["IsOfferPlayForFun"]),
                            VendorID = DataUtils.ToString(reader["VendorID"]),
                            OrderNo = DataUtils.ToString(reader["OrderNo"])
                        });
                    }
                    
                }

                return new ProductModel
                {
                    ProductEntities = entities,
                    TotalRows = DataUtils.ToInt32(objDb.GetOuputParams()["totalrows"])
                };
            }
        }

        public IEnumerable<BetTypeGroupEntity> SelectBetTypeGroup(SelectBetTypeGroupCriteria selectBetTypeGroupCriteria)
        {
            var entities = new List<BetTypeGroupEntity>();
            using (var objDb = new ObjectDb(selectBetTypeGroupCriteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(selectBetTypeGroupCriteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        entities.Add(new BetTypeGroupEntity
                        {
                            BetTypeGroupId = DataUtils.ToString(reader["typeid"]),
                            BetTypeGroupName = DataUtils.ToString(reader["typenamee"])
                        });
                    }
                    return entities;
                }
            }
        }

        public IEnumerable<SportTypeGroupEntity> SelectSportTypeGroup(SelectSportTypeGroupCriteria selectSportTypeGroupCriteria)
        {
            var entities = new List<SportTypeGroupEntity>();
            using (var objDb = new ObjectDb(selectSportTypeGroupCriteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(selectSportTypeGroupCriteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        entities.Add(new SportTypeGroupEntity
                        {
                            SportTypeGroupId = DataUtils.ToString(reader["SportTypeGroup"]),
                            SportTypeGroupName = DataUtils.ToString(reader["DisplaySportName"]),
                            ProductId = reader["ProductID"] == DBNull.Value ? null : DataUtils.ToString(reader["ProductID"])
                        });
                    }
                    return entities;
                }
            }
        }

        public Result Update(UpdateProductCriteria criteria) => base.ExecuteNonQuery(criteria);
    }
}