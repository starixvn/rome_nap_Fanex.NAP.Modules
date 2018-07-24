namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using Core.Criteria;
    using Core.Models;
    using Core.Services.Impl;
    using Core.Services.Interface;

    internal class PermissionService
    {
        public Result CheckSpecialSitePermission(int userId, int? custId = null, string username = null)
        {
            IServiceBase _serviceBase = new ServiceBase();

            return _serviceBase.CheckSpecialSitePermission(
                new CheckSpecialSitePermissionCriteria
                {
                    CustID = custId,
                    CustName = username,
                    UserID = userId
                });
        }
    }
}