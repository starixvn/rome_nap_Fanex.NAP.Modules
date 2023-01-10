namespace Fanex.Tarus.Admin.ManageProductSetting.Services
{
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig;
    using Fanex.Tarus.Admin.ManageProductSetting.DataAccess;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities.UMConfig;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageUMConfig;
    using Fanex.Tarus.Admin.ViewModel;
    using Fanex.Tarus.Infrastructure.Common;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public interface IManageUMConfigService
    {
        IEnumerable<UMConfigurationEntity> GetAllUMConfiguration(GetUMConfigRequest request);

        IEnumerable<ProductSettingByFunctionEntity> GetProductSettingByFunction();

        Result UpdateUMConfiguration(UMConfigurationEntity entity);

        Result InsertUMConfiguration(UMConfigurationEntity entity);

        Result DeleteUMConfiguration(int configId);

        Result DeleteOldData();

        bool CheckIsInUMTime(int configId);

        IEnumerable<SiteViewModel> GetAllSites();

        IEnumerable<SiteViewModel> GetMarkets();
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

        public IEnumerable<SiteViewModel> GetAllSites()
        {
            var sites = new List<SiteViewModel>();
            dataAccess.GetAllSites(new GetAllSitesCriteria(AppSession.AccId))
               .GroupBy(p => p.SiteLabel)
               .ToList()
               .ForEach(grp =>
               {
                   if (string.IsNullOrEmpty(grp.Key))
                   {
                       sites.Add(new SiteViewModel("Deposit", "Deposit", grp.Select(p => new SiteViewModel(p.SiteId.ToString(), $"[{p.SiteId.ToString()}] {p.SiteName}", null))));
                   }
                   else
                   {
                       sites.Add(new SiteViewModel(grp.Key, grp.Key, grp.Select(p => new SiteViewModel(p.SiteId.ToString(), $"[{p.SiteId.ToString()}] {p.SiteName}", null))));
                   }
               });

            return sites.OrderBy(x => x.Name == "Deposit");
        }

        public IEnumerable<SiteViewModel> GetMarkets()
            => dataAccess.GetMarkets(new GetMarketsCriteria())
                .Select(i => new SiteViewModel(i.Id.ToString(), i.Name));

        public IEnumerable<UMConfigurationEntity> GetAllUMConfiguration(GetUMConfigRequest request)
            => dataAccess.GetAllUMConfiguration(new GetAllUMConfigurationCriteria(request.FunctionType, request.BeginTime, request.EndTime));

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
