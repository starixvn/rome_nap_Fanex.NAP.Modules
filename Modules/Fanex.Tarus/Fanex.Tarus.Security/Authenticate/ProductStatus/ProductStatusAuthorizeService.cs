using System.Web.Mvc;
using Fanex.ProductStatus.Setting.Services;

namespace Fanex.Tarus.Security.Authenticate.ProductStatus
{
    public class ProductStatusAuthorizeService : IProductStatusAuthorizeService
    {
        private readonly CustomerSettingAttribute customerSettingAttribute;

        public ProductStatusAuthorizeService()
        {
            customerSettingAttribute = new CustomerSettingAttribute();
        }

        public bool CanView(AuthorizationContext filterContext)
        {
            var actionName = filterContext.GetActionName();

            return customerSettingAttribute.AllowRead(actionName);
        }

        public bool CanUpdate(AuthorizationContext filterContext)
        {
            var actionName = filterContext.GetActionName();

            return customerSettingAttribute.AllowUpdate(actionName);
        }
    }
}
