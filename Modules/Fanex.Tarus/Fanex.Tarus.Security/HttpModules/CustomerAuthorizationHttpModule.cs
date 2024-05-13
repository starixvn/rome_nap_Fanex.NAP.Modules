namespace Fanex.Tarus.Security.HttpModules
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Configurations;
    using Constants;
    using Fanex.Tarus.Security.Services;
    using Models;
    using Services;

    public sealed class CustomerAuthorizationHttpModule : IHttpModule
    {
        private readonly UrlPermissionHttpContext currentContext;
        private readonly ICustomerAuthorizationService customerAuthorizationService;

        public CustomerAuthorizationHttpModule()
            : this(null, null)
        {
        }

        internal CustomerAuthorizationHttpModule(UrlPermissionHttpContext currentContext, ICustomerAuthorizationService customerAuthorizationService)
        {
            this.currentContext = currentContext ?? new UrlPermissionHttpContext();
            this.customerAuthorizationService = customerAuthorizationService ?? new CustomerAuthorizationService();
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            if (context != null)
            {
                context.AcquireRequestState += Context_AcquireRequestState;
            }
        }

        private void Context_AcquireRequestState(object sender, EventArgs e)
        {
            var urlPermission = GetUrlPermission();

            if (urlPermission == null)
            {
                return;
            }

            var paramValue = GetParamValue(urlPermission.ParamNames);

            if (paramValue == null)
            {
                return;
            }

            Authorize(urlPermission, paramValue);
        }

        private UrlPermission GetUrlPermission()
        {
            var relativePath = currentContext.GetLowerAbsolutePath();

            return UrlPermissionManager.GetUrlPermission(relativePath);
        }

        private string GetParamValue(IEnumerable<string> paramNames)
        {
            var paramValue = currentContext.GetParamValueFromParamList(paramNames);

            return HttpUtility.UrlDecode(paramValue);
        }

        private void Authorize(UrlPermission urlPermission, string paramValue)
        {
            var customerTypes = customerAuthorizationService.GetCustomerTypes(paramValue);
            foreach (var filter in urlPermission.Filters)
            {
              
                var authorize = CustomerAuthorizationFactory.GetInstance(filter, paramValue, customerTypes);
                if (authorize == null) continue;
                var result = urlPermission.Policy == PolicyConst.Read
                    ? authorize.CanRead(paramValue, currentContext.UserId)
                    : authorize.CanHost(paramValue, currentContext.UserId);

                if (!result.IsOk)
                {
                    currentContext.RedirectToAccessDenyPage(result);
                }
            }
        }
    }
}
