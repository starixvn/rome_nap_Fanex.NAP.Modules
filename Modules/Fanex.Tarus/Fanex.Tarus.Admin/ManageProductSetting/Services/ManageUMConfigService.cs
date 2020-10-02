namespace Fanex.Tarus.Admin.ManageProductSetting.Services
{
    using System.Collections.Generic;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig;
    using Fanex.Tarus.Admin.ManageProductSetting.DataAccess;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities.UMConfig;

    public interface IManageUMConfigService
    {
        IEnumerable<ProductSettingByFunctionEntity> GetProductSettingByFunction();
    }
    public class ManageUMConfigService : IManageUMConfigService
    {
        private readonly IManageUMConfigDataAccess dataAccess;

        public ManageUMConfigService()
        {
            dataAccess = new ManageUMConfigDataAccess();
        }

        public IEnumerable<ProductSettingByFunctionEntity> GetProductSettingByFunction()
            => dataAccess.GetProductSettingByFunction(new GetProductSettingByFunctionCriteria());
    }
}
