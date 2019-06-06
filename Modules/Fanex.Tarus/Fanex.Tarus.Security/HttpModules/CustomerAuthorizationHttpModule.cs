namespace Fanex.Tarus.Security.HttpModules
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Configurations;
    using Constants;
    using Models;
    using Services;

    public sealed class CustomerAuthorizationHttpModule : IHttpModule
    {
        private readonly UrlPermissionHttpContext currentContext;

        public CustomerAuthorizationHttpModule()
            : this(null)
        {
        }

        internal CustomerAuthorizationHttpModule(UrlPermissionHttpContext currentContext)
        {
            this.currentContext = new UrlPermissionHttpContext();
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

            return paramValue != null
                ? HttpUtility.UrlDecode(paramValue)
                : paramValue;
        }

        private void Authorize(UrlPermission urlPermission, string paramValue)
        {
            ICustomerAuthorization authorize;
            AuthorizationResult result;

            foreach (var filter in urlPermission.Filters)
            {
                authorize = CustomerAuthorizationFactory.GetInstance(filter);

                if (authorize != null)
                {
                    result = urlPermission.Policy == PolicyConst.Read
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
}
