namespace Fanex.Tarus.Admin.ManageProductSetting.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.DataAccess;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities.UMConfig;
    using Fanex.Tarus.Infrastructure.Helper;

    public interface IManageUMConfigDataAccess
    {
        IEnumerable<UMConfigurationEntity> GetAllUMConfiguration(ICriteria criteria);

        IEnumerable<ProductSettingByFunctionEntity> GetProductSettingByFunction(ICriteria criteria);

        Result UpdUMConfiguration(UpdUMConfigurationCriteria criteria);

        Result InsertUMConfiguration(InsertUMConfigurationCriteria criteria);

        Result DeleteUMConfiguration(DeleteUMConfigurationCriteria criteria);

        Result DeleteOldData(DeleteOldDateCriteria criteria);

        bool CheckIsInUMTime(CheckInUMCriteria criteria);
    }
    public class ManageUMConfigDataAccess : BaseDataAccess, IManageUMConfigDataAccess
    {
        public bool CheckIsInUMTime(CheckInUMCriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                var outputParams = db.GetOuputParams();

                if (outputParams != null)
                {
                    return DataUtils.ToBoolean(outputParams["isinum"]);
                }
            }

            return false;
        }

        public Result DeleteOldData(DeleteOldDateCriteria criteria)
         => base.ExecuteNonQuery(criteria);

        public Result DeleteUMConfiguration(DeleteUMConfigurationCriteria criteria)
         => base.ExecuteNonQuery(criteria);

        public IEnumerable<UMConfigurationEntity> GetAllUMConfiguration(ICriteria criteria)
        {
            var entities = new List<UMConfigurationEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new UMConfigurationEntity
                    {
                        ConfigId = DataUtils.ToInt32(reader["configId"]),
                        FunctionType = DataUtils.ToInt16(reader["FunctionType"]),
                        ProductId = DataUtils.ToInt16(reader["ProductId"]),
                        SportType = DataUtils.ToInt16(reader["SportType"]),
                        SettingType = DataUtils.ToInt16(reader["SettingType"]),
                        BetType = DataUtils.ToInt16(reader["BetType"]),
                        IsLive = DataUtils.ToBoolean(reader["IsLive"]),
                        SiteId = DataUtils.ToInt32(reader["SiteId"]),
                        Site = DataUtils.ToString(reader["Site"]),
                        Currency = DataUtils.ToInt32(reader["currency"]),
                        CurrencyName = DataUtils.ToString(reader["CurrencyName"]),
                        FunctionTypeName = DataUtils.ToString(reader["FunctionTypeName"]),
                        ProductName = DataUtils.ToString(reader["ProductName"]),
                        SportName = DataUtils.ToString(reader["SportName"]),
                        SettingName = DataUtils.ToString(reader["SettingName"]),
                        LockAddingNew = DataUtils.ToBoolean(reader["LockAddingNew"]),
                        ShowTime = DataUtils.ToString(reader["ShowTime"]),
                        BeginTime = DataUtils.ToString(reader["BeginTime"]),
                        EndTime = DataUtils.ToString(reader["EndTime"]),
                        Username = DataUtils.ToString(reader["Username"]),
                        SuperId = DataUtils.ToInt32(reader["SuperId"])
                    });
                }

                reader.Close();
            }

            return entities;
        }

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
                        IsLive = DataUtils.ToBoolean(reader["IsLive"]),
                        FunctionTypeName = DataUtils.ToString(reader["FunctionTypeName"]),
                        ProductName = DataUtils.ToString(reader["ProductName"]),
                        SportName = DataUtils.ToString(reader["SportName"]),
                        SettingName = DataUtils.ToString(reader["SettingName"])
                    });
                }

                reader.Close();
            }
            var result = new List<ProductSettingByFunctionEntity>();
            var functionGroups = entities
                .GroupBy(entity => (SportType: entity.SportType, SportName: entity.SportName, ProductId: entity.ProductId, ProductName: entity.ProductName, FunctionType: entity.FunctionType, FunctionTypeName: entity.FunctionTypeName))
                .GroupBy(entity => (ProductId: entity.Key.ProductId, ProductName: entity.Key.ProductName, FunctionType: entity.Key.FunctionType, FunctionTypeName: entity.Key.FunctionTypeName))
                .GroupBy(group => (FunctionType: group.Key.FunctionType, FunctionTypeName: group.Key.FunctionTypeName));
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
                    FunctionType = functionGroup.Key.FunctionType,
                    FunctionTypeName = functionGroup.Key.FunctionTypeName,
                    Products = products
                });
            }

            return result;
        }

        public Result InsertUMConfiguration(InsertUMConfigurationCriteria criteria)
        {
            var entities = new List<InsertErrorEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new InsertErrorEntity
                    {
                        Username = DataUtils.ToString(reader["Username"]),
                        ErrorCode = DataUtils.ToInt16(reader["ErrorCode"]),
                        ErrorMsg = DataUtils.ToString(reader["ErrorMsg"]),
                    });
                }
                reader.Close();
            }
            var groupErrors = entities.GroupBy(entity => (ErrorCode: entity.ErrorCode, ErrorMsg: entity.ErrorMsg));
            var errors = new List<string>();
            foreach (var groupError in groupErrors)
            {
                var usernames = string.Join(",", groupError.Select(error => error.Username));
                var errorMessage = $"{groupError.Key.ErrorMsg}: {usernames}";
                errors.Add(errorMessage);
            }

            if(errors.Count > 0)
            {
                return new Result(1, string.Join(". ", errors));
            }

            return new Result();
        }

        public Result UpdUMConfiguration(UpdUMConfigurationCriteria criteria)
         => base.ExecuteNonQuery(criteria);
    }
}
