using Fanex.Tarus.Security.Models;
using System.Collections.Generic;

namespace Fanex.Tarus.Security.HttpModules.Services
{
    public static class CustomerAuthorizationFactory
    {
        public static ICustomerAuthorization GetInstance(string type, string paramValue, IEnumerable<CustomerType> customerTypes)
        {
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
