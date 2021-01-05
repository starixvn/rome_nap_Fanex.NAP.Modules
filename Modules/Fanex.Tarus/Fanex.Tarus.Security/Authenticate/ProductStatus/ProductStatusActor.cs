using Fanex.CustomerSetting.Module.Core.Web;
using Fanex.ProductStatus.Setting.Services;
using Sunplus.Accounting.Core.Configurations;
using Sunplus.Accounting.Core.Helper;

namespace Fanex.Tarus.Security.Authenticate.ProductStatus
{
    public class ProductStatusActor : IProductStatusActor
    {
        public int Id => AppSession.AccId;

        public string Name => AppSession.AccUserName;

        public string ClientIP => Utils.GetClientIP();

        public byte SystemId => Sunplus.Accounting.Core.Common.AccCommon.SystemId;

        public byte SettingStatus => WebSettings.SettingStatus;
    }
}
