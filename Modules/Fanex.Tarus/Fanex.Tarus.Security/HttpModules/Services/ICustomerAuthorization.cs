namespace Fanex.Tarus.Security.HttpModules.Services
{
    using Fanex.Tarus.Security.Models;

    internal interface ICustomerAuthorization
    {
        AuthorizationResult CanRead(string parameterValue, int userId);

        AuthorizationResult CanHost(string parameterValue, int userId);
    }
}
