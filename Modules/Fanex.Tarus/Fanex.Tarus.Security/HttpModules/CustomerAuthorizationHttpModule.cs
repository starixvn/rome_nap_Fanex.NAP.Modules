namespace Fanex.Tarus.Security.HttpModules
{
    using System;
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

            var paramValue = currentContext.GetParamValueFromParamList(urlPermission.ParamNames);

            if (paramValue == null)
            {
                return;
            }

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

        private UrlPermission GetUrlPermission()
        {
            var relativePath = currentContext.GetLowerAbsolutePath();

            return UrlPermissionManager.GetUrlPermission(relativePath);
        }
    }
}
