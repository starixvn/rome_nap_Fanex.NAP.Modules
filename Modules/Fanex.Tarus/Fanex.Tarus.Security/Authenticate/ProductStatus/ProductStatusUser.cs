using System.Collections.Generic;
using System.Linq;
using Fanex.CustomerSetting.Module.Core.Web;
using Fanex.ProductStatus.Setting.Services;
using Sunplus.Accounting.Core.Helper;

namespace Fanex.Tarus.Security.Authenticate.ProductStatus
{
    public class ProductStatusUser : IProductStatusUserService
    {
        public int GetActorId()
            => AppSession.AccId;

        public string GetClientIP()
            => Utils.GetClientIP();

        public byte GetSystemId()
            => Sunplus.Accounting.Core.Common.AccCommon.SystemId;

        public IEnumerable<KeyValuePair<int, string>> GetProductList()
        {
            return Enumerable.Empty<KeyValuePair<int, string>>();
        }
    }
}
