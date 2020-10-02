namespace Fanex.Tarus.Admin.ManageProductSetting.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.DataAccess;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities.UMConfig;
    using Fanex.Tarus.Infrastructure.Helper;

    public interface IManageUMConfigDataAccess
    {
        IEnumerable<ProductSettingByFunctionEntity> GetProductSettingByFunction(ICriteria criteria);
    }
    public class ManageUMConfigDataAccess : BaseDataAccess, IManageUMConfigDataAccess
    {
        public IEnumerable<ProductSettingByFunctionEntity> GetProductSettingByFunction(ICriteria criteria)
        {
            var entities = new List<UMConfigSettingEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new UMConfigSettingEntity
                    {
                        FunctionType = DataUtils.ToString(reader["FunctionType"]),
                        ProductId = DataUtils.ToString(reader["ProductId"]),
                        SportType = DataUtils.ToString(reader["SportType"]),
                        SettingType = DataUtils.ToString(reader["SettingType"]),
                        BetType = DataUtils.ToString(reader["BetType"]),
                        IsLive = DataUtils.ToString(reader["IsLive"]),
                        ProductName = DataUtils.ToString(reader["ProductName"]),
                        SportName = DataUtils.ToString(reader["SportName"]),
                        SettingName = DataUtils.ToString(reader["SettingName"])
                    });
                }

                reader.Close();
            }
            var result = new List<ProductSettingByFunctionEntity>();
            var functionGroups = entities
                .GroupBy(entity => (SportType: entity.SportType, SportName: entity.SportName, ProductId: entity.ProductId, ProductName: entity.ProductName, FunctionType: entity.FunctionType))
                .GroupBy(entity => (ProductId: entity.Key.ProductId, ProductName: entity.Key.ProductName, FunctionType: entity.Key.FunctionType))
                .GroupBy(group => group.Key.FunctionType);
            foreach (var functionGroup in functionGroups)
            {
                var products = new List<ProductSettingEntity>();

                foreach (var product in functionGroup)
                {
                    var sports = new List<SportSettingEntity>();
                    foreach (var setting in product)
                    {
                        sports.Add(new SportSettingEntity
                        {
                            Settings = setting,
                            SportType = setting.Key.SportType,
                            SportName = setting.Key.SportName
                        });
                    }

                    products.Add(new ProductSettingEntity
                    {
                        ProductId = product.Key.ProductId,
                        ProductName = product.Key.ProductName,
                        Sports = sports
                    });
                }

                result.Add(new ProductSettingByFunctionEntity
                {
                    FunctionType = functionGroup.Key,
                    Products = products
                });
            }

            return result;
        }
    }
}
