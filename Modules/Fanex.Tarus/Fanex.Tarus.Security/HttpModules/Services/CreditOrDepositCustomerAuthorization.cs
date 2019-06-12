namespace Fanex.Tarus.Security.HttpModules.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Models;
    using Security.Services;

    public sealed class CreditOrDepositCustomerAuthorization : ICustomerAuthorization
    {
        private const string CN88SiteName = "cn88";
        private readonly ICustomerAuthorizationService customerAuthorizationService;

        public CreditOrDepositCustomerAuthorization()
            : this(null)
        {
        }

        internal CreditOrDepositCustomerAuthorization(ICustomerAuthorizationService customerAuthorizationService)
        {
            this.customerAuthorizationService = customerAuthorizationService ?? new CustomerAuthorizationService();
        }

        public AuthorizationResult CanRead(string parameterValue, int userId)
            => CanAccess(parameterValue, userId, PolicyConst.Read);

        public AuthorizationResult CanHost(string parameterValue, int userId)
            => CanAccess(parameterValue, userId, PolicyConst.Host);

        public AuthorizationResult CanAccess(string parameterValue, int userId, byte urlPolicy)
        {
            var customerSites = customerAuthorizationService.GetCustomerSites(parameterValue).ToList();

            var permissions = MangeAccountInfoPermissionBuilder.GetManageAccountInfoPermissions(userId, urlPolicy);
            var hasCN88SitePermission = permissions.ContainsKey(ManageAccountInfoConst.CN88SiteFunctionName);
            AuthorizationResult result = AuthorizationResult.Ok();

            if (hasCN88SitePermission)
            {
                if (HasCreditSiteOrDepositSitePermission(permissions))
                {
                    if (customerSites.Any(c => !c.Site.Equals(CN88SiteName, StringComparison.OrdinalIgnoreCase)))
                    {
                        return CheckCreditSiteOrDepositSitePermission(permissions, parameterValue);
                    }
                }
                else
                {
                    result = CheckCN88SitePermission(customerSites, false);
                }
            }
            else
            {
                result = CheckCN88SitePermission(customerSites, true);

                if (result.IsOk)
                {
                    return CheckCreditSiteOrDepositSitePermission(permissions, parameterValue);
                }
            }

            return result;
        }

        private AuthorizationResult CheckCN88SitePermission(IEnumerable<CustomerSite> customerSites, bool isCN88Site)
        {
            var customers = customerSites.Where(c => c.Site.Equals(CN88SiteName, StringComparison.OrdinalIgnoreCase) == isCN88Site).ToList();
            var customersCount = customers.Count;

            if (customersCount > 0)
            {
                return customersCount == 1
                    ? AuthorizationResult.Forbidden()
                    : AuthorizationResult.Forbidden(BuildInvalidUsernamesError(customers.Select(c => c.CustInfo)));
            }

            return AuthorizationResult.Ok();
        }

        private static bool HasCreditSiteOrDepositSitePermission(IDictionary<string, byte> permissions)
            => permissions.ContainsKey(ManageAccountInfoConst.CreditSiteFunctionName)
            || permissions.ContainsKey(ManageAccountInfoConst.DepositSiteFunctionName);

        private AuthorizationResult CheckCreditSiteOrDepositSitePermission(
            IDictionary<string, byte> permissions,
            string parameterValue)
        {
            var customerTypes = customerAuthorizationService.GetCustomerTypes(parameterValue).ToList();
            var customerTypesCount = customerTypes.Count;

            if (customerTypesCount == 1)
            {
                return CanAccess(permissions, customerTypes[0]);
            }
            else if (customerTypesCount > 1)
            {
                return CanAccess(permissions, customerTypes);
            }

            return AuthorizationResult.Ok();
        }

        private static AuthorizationResult CanAccess(
            IDictionary<string, byte> permissions,
            CustomerType customerType)
        {
            var functionName = customerType.IsDeposit
                ? ManageAccountInfoConst.DepositSiteFunctionName
                : ManageAccountInfoConst.CreditSiteFunctionName;

            return permissions.ContainsKey(functionName)
                ? AuthorizationResult.Ok()
                : AuthorizationResult.Forbidden();
        }

        private static AuthorizationResult CanAccess(
            IDictionary<string, byte> permissions,
            IEnumerable<CustomerType> customerTypes)
        {
            ICollection<string> creditCustomers = new List<string>();
            ICollection<string> depositCustomers = new List<string>();

            foreach (var customer in customerTypes)
            {
                if (customer.IsDeposit)
                {
                    depositCustomers.Add(customer.CustInfo);
                }
                else
                {
                    creditCustomers.Add(customer.CustInfo);
                }
            }

            if (creditCustomers.Count > 0 && !permissions.ContainsKey(ManageAccountInfoConst.CreditSiteFunctionName))
            {
                return AuthorizationResult.Forbidden(BuildInvalidUsernamesError(creditCustomers));
            }

            if (depositCustomers.Count > 0 && !permissions.ContainsKey(ManageAccountInfoConst.DepositSiteFunctionName))
            {
                return AuthorizationResult.Forbidden(BuildInvalidUsernamesError(depositCustomers));
            }

            return AuthorizationResult.Ok();
        }

        private static string BuildInvalidUsernamesError(IEnumerable<string> usernames)
            => $"Invalid usernames: {string.Join(", ", usernames)}";
    }
}
