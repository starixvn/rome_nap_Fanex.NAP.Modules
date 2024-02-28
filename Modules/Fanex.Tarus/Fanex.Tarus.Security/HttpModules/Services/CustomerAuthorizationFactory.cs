using Fanex.Tarus.Security.Models;
using Fanex.Tarus.Security.Services;
using System.Collections.Generic;

namespace Fanex.Tarus.Security.HttpModules.Services
{
    public static class CustomerAuthorizationFactory
    {
        private static readonly ICustomerAuthorizationService CustomerAuthorizationService = new CustomerAuthorizationService();

        public static ICustomerAuthorization GetInstance(string type, string paramValue)
        {
            var customerTypes = CustomerAuthorizationService.GetCustomerTypes(paramValue);
            switch (type)
            {
                case "[A]":
                    return new SpecialSiteCustomerAuthorization(customerTypes);

                case "[B]":
                    return new CreditOrDepositCustomerAuthorization(customerTypes);

                case "[C]":
                    return new FinanceCustomerAuthorization(customerTypes);

                default:
                    return null;
            }
        }
    }
}
