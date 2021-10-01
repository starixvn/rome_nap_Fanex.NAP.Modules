using Fanex.Tarus.Security.Models;
using Fanex.Tarus.Security.Services;
using System.Collections.Generic;

namespace Fanex.Tarus.Security.HttpModules.Services
{
    public static class CustomerAuthorizationFactory
    {
        private static readonly ICustomerAuthorizationService customerAuthorizationService = new CustomerAuthorizationService();

        public static ICustomerAuthorization GetInstance(string type, string paramValue)
        {
            var customerTypes = customerAuthorizationService.GetCustomerTypes(paramValue);
            var customerSites = customerAuthorizationService.GetCustomerSites(paramValue);
            switch (type)
            {
                case "[A]":
                    return new SpecialSiteCustomerAuthorization(customerTypes);

                case "[B]":
                    return new CreditOrDepositCustomerAuthorization(customerSites, customerTypes);

                case "[C]":
                    return new FinanceCustomerAuthorization(customerSites, customerTypes);

                default:
                    return null;
            }
        }
    }
}
