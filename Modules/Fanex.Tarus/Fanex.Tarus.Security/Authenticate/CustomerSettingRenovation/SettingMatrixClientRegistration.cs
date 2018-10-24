namespace Fanex.Tarus.Security.Authenticate
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using CustomerSetting.MatrixClient;
    using HttpModules.Services;
    using Sunplus.Accounting.Core.Common;

    public class SettingMatrixClientRegistration : SettingMatrixRegistration
    {
        private readonly CustomerSettingAttribute _customerSettingAttribute = new CustomerSettingAttribute();
        private readonly ICustomerAuthorization _specialSiteAuthorization = new SpecialSiteCustomerAuthorization();

        public override bool VerifyPermission()
        {
            var hasReadPermission = _customerSettingAttribute.AllowRead(GetActionName());

            if (!hasReadPermission)
            {
                return false;
            }

            var customerName = HttpContext.Current.Request.QueryString["customerName"];

            if (!string.IsNullOrWhiteSpace(customerName))
            {
                var userId = AppSession.AccId;

                return _specialSiteAuthorization.CanRead(customerName, userId).IsOk;
            }

            return true;
        }

        public override void RegisterRouteConfig(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Matrix_default",
                url: "Matrix/{controller}/{action}/{id}",
                defaults: new { controller = "ViewCustomerSettingMatrix", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Fanex.CustomerSetting.MatrixClient.Controllers" });
        }

        private string GetActionName()
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            if (routeValues != null)
            {
                if (routeValues.ContainsKey("action"))
                {
                    var controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
                    var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
                    return controllerName + '/' + actionName;
                }
            }

            return string.Empty;
        }
    }
}