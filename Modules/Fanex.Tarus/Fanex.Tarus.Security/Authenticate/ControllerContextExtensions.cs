using System.Web.Mvc;

namespace Fanex.Tarus.Security.Authenticate
{
    public static class ControllerContextExtensions
    {
        public static string GetActionName(this ControllerContext filterContext)
        {
            var valueProvider = filterContext.Controller.ValueProvider;

            return string.Format(
                "{0}/{1}",
                GetActionParameterValue(valueProvider, "controller"),
                GetActionParameterValue(valueProvider, "action"));
        }

        private static string GetActionParameterValue(IValueProvider valueProvider, string parameterName)
        {
            var valueProviderResult = valueProvider.GetValue(parameterName);

            return valueProviderResult != null
                ? valueProviderResult.AttemptedValue
                : string.Empty;
        }
    }
}
