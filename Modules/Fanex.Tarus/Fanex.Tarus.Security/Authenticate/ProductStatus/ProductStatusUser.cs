using System;
using Fanex.CustomerSetting.Module.Core.Web;
using Fanex.ProductStatus.Setting.Services;
using Sunplus.Accounting.Core.Helper;

namespace Fanex.Tarus.Security.Authenticate.ProductStatus
{
    public class ProductStatusUser : IProductStatusUser
    {
        public Func<int> GetActorId()
            => () => AppSession.AccId;

        public Func<string> GetClientIP()
            => () => Utils.GetClientIP();
    }
}
