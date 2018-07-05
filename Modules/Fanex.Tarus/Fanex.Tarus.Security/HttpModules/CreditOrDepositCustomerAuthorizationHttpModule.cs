namespace Fanex.Tarus.Security.HttpModules
{
    using System;
    using System.Web;
    using Configurations;
    using Security.Models;
    using Services;

    public sealed class CreditOrDepositCustomerAuthorizationHttpModule : IHttpModule
    {
        private readonly UrlPermissionHttpContext currentContext;
        private readonly ICreditOrDepositCustomerAuthorization customerAuthorization;

        public CreditOrDepositCustomerAuthorizationHttpModule()
            : this(null, null)
        {
        }

        internal CreditOrDepositCustomerAuthorizationHttpModule(
            UrlPermissionHttpContext currentContext,
            ICreditOrDepositCustomerAuthorization customerAuthorization)
        {
            this.currentContext = new UrlPermissionHttpContext();
            this.customerAuthorization = customerAuthorization ?? new CreditOrDepositCustomerAuthorization();
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

            if (urlPermission != null)
            {
                var paramValue = currentContext.GetParamValueFromParamList(urlPermission.ParamNames);

                if (paramValue != null)
                {
                    var userPolicy = customerAuthorization.GetPolicy(paramValue, currentContext.UserId);

                    if (userPolicy == null || userPolicy.Value < urlPermission.Policy)
                    {
                        currentContext.RedirectToAccessDenyPage();
                    }
                }
            }
        }

        private UrlPermission GetUrlPermission()
        {
            var relativePath = currentContext.GetLowerAbsolutePath();

            return UrlPermissionManager.GetUrlPermission(relativePath);
        }

        private static ICreditOrDepositCustomerAuthorization GetFilter(string filter)
        {
            switch (filter)
            {
                //case "[A]":
                //    return new CreditOrDepositCustomerAuthorization();

                case "[B]":
                    return new CreditOrDepositCustomerAuthorization();

                default:
                    return null;
            }
        }
    }
}
