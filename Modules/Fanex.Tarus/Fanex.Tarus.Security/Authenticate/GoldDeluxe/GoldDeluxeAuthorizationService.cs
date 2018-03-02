namespace Fanex.Tarus.Security.Authenticate
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Fanex.GoldDeluxe.CoreSetting.Services;

    public class GoldDeluxeAuthorizationService : IGoldDeluxeAuthorizationService
    {
        private CustomerSettingAttribute _customerSettingAttribute = new CustomerSettingAttribute();

        public bool IsAuthorizedRead(AuthorizationContext authorizationContext, int custId)
        {
            var actionName = authorizationContext.GetActionName();
            return _customerSettingAttribute.AllowRead(actionName);
        }

        public bool IsAuthorizedHost(AuthorizationContext authorizationContext, int custId)
        {
            var actionName = authorizationContext.GetActionName();
            return _customerSettingAttribute.AllowUpdate(actionName);
        }

        public bool IsAuthorizedReadMultiple(AuthorizationContext authorizationContext, IEnumerable<string> usernames)
        {
            return true;
        }

        public bool IsAuthorizedHostMultiple(AuthorizationContext authorizationContext, IEnumerable<string> usernames)
        {
            return true;
        }
    }
}
