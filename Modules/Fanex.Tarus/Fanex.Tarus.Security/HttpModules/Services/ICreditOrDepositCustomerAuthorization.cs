namespace Fanex.Tarus.Security.HttpModules.Services
{
    using System.Collections.Generic;

    public interface ICreditOrDepositCustomerAuthorization
    {
        bool CanRead(string custIdOrUserName, int userId);

        bool CanHost(string custIdOrUserName, int userId);

        byte? GetPolicy(string custIdOrUserName, int userId);

        IDictionary<string, byte> GetCreditAndDepositPolicy(int userId);
    }
}
