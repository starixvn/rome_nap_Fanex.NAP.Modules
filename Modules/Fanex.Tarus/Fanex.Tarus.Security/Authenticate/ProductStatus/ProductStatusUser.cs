using Fanex.CustomerSetting.Module.Core.Web;
using Fanex.ProductStatus.Setting.Services;
using Sunplus.Accounting.Core.Helper;

namespace Fanex.Tarus.Security.Authenticate.ProductStatus
{
    public class ProductStatusUser : IProductStatusUser
    {
        public int ActorId
        {
            get
            {
                return AppSession.AccId;
            }
        }

        public string IP
        {
            get
            {
                return Utils.GetClientIP();
            }
        }
    }
}
