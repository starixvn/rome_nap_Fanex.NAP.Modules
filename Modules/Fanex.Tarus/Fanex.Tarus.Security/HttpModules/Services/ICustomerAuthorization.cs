namespace Fanex.Tarus.Security.HttpModules.Services
{
    using Models;

    public interface ICustomerAuthorization
    {
        AuthorizationResult CanRead(string parameterValue, int userId);

        AuthorizationResult CanHost(string parameterValue, int userId);
    }
}
