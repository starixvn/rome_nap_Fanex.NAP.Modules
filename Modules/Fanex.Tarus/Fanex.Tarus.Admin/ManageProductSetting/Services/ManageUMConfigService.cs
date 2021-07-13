namespace Fanex.Tarus.Admin.ManageProductSetting.Services
{
    using System.Collections.Generic;
    using System.Web;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.SMAManagement;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig;
    using Fanex.Tarus.Admin.ManageProductSetting.DataAccess;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities.UMConfig;
    using Fanex.Tarus.Infrastructure.Common;

    public interface IManageUMConfigService
    {
        IEnumerable<UMConfigurationEntity> GetAllUMConfiguration();

        IEnumerable<ProductSettingByFunctionEntity> GetProductSettingByFunction();

        Result UpdateUMConfiguration(UMConfigurationEntity entity);

        Result InsertUMConfiguration(UMConfigurationEntity entity);

        Result DeleteUMConfiguration(int configId);

        Result DeleteOldData();

        bool CheckIsInUMTime(int configId);

        IEnumerable<IdNameEntity> GetAllSites();
    }

    public class ManageUMConfigService : IManageUMConfigService
    {
        private readonly IManageUMConfigDataAccess dataAccess;

        public ManageUMConfigService()
        {
            dataAccess = new ManageUMConfigDataAccess();
        }

        public bool CheckIsInUMTime(int configId)
            => dataAccess.CheckIsInUMTime(new CheckInUMCriteria { ConfigId = configId });

        public Result DeleteOldData()
            => dataAccess.DeleteOldData(new DeleteOldDateCriteria { UserId = AppSession.AccId });

        public Result DeleteUMConfiguration(int configId)
            => dataAccess.DeleteUMConfiguration(new DeleteUMConfigurationCriteria
            {
                ConfigId = configId,
                UserId = AppSession.AccId,
                IP = HttpContext.Current.Request.UserHostAddress
            });

        public IEnumerable<IdNameEntity> GetAllSites()
            => dataAccess.GetAllSites(new GetAllSitesCriteria(AppSession.AccId));

        public IEnumerable<UMConfigurationEntity> GetAllUMConfiguration()
            => dataAccess.GetAllUMConfiguration(new GetAllUMConfigurationCriteria());

        public IEnumerable<ProductSettingByFunctionEntity> GetProductSettingByFunction()
            => dataAccess.GetProductSettingByFunction(new GetProductSettingByFunctionCriteria());

        public Result InsertUMConfiguration(UMConfigurationEntity entity)
        {
            var criteria = new InsertUMConfigurationCriteria
            {
                UserId = AppSession.AccId,
                IP = HttpContext.Current.Request.UserHostAddress,
                FunctionType = entity.FunctionType,
                ProductId = entity.ProductId,
                SportType = entity.SportType,
                SettingType = entity.SettingType,
                SiteId = entity.SiteId,
                Usernames = entity.Username,
                Currency = entity.Currency,
                BetType = entity.BetType,
                IsLive = entity.IsLive,
                BeginTime = entity.BeginTime,
                EndTime = entity.EndTime,
                ShowTime = entity.ShowTime,
                LockAddingNew = entity.LockAddingNew
            };

            return dataAccess.InsertUMConfiguration(criteria);
        }

        public Result UpdateUMConfiguration(UMConfigurationEntity entity)
        {
            var criteria = new UpdUMConfigurationCriteria
            {
                UserId = AppSession.AccId,
                IP = HttpContext.Current.Request.UserHostAddress,
                ConfigID = entity.ConfigId,
                BeginTime = entity.BeginTime,
                EndTime = entity.EndTime,
                ShowTime = entity.ShowTime,
                LockAddingNew = entity.LockAddingNew
            };

            return dataAccess.UpdUMConfiguration(criteria);
        }
    }
}
