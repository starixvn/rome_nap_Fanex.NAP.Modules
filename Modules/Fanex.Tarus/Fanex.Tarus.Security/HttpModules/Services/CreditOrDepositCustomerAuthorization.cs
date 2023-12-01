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
        private readonly IEnumerable<CustomerType> _customerTypes;

        public CreditOrDepositCustomerAuthorization()
            : this(null)
        {
        }

        internal CreditOrDepositCustomerAuthorization(IEnumerable<CustomerType> customerTypes)
        {
            this._customerTypes = customerTypes;
        }

        public AuthorizationResult CanRead(string parameterValue, int userId)
            => CanAccess(parameterValue, userId, PolicyConst.Read);

        public AuthorizationResult CanHost(string parameterValue, int userId)
            => CanAccess(parameterValue, userId, PolicyConst.Host);

        public AuthorizationResult CanAccess(string parameterValue, int userId, byte urlPolicy)
        {
            AuthorizationResult result = AuthorizationResult.Ok();
            if (IsNeedToCheck())
            {
                var permissions = MangeAccountInfoPermissionBuilder.GetManageAccountInfoPermissions(userId, urlPolicy);
                return CheckCreditSiteOrDepositSitePermission(permissions);
            }

            return result;
        }

        private AuthorizationResult CheckCreditSiteOrDepositSitePermission(
            IDictionary<string, byte> permissions)
        {
            var customerTypesCount = _customerTypes.Count();

            if (customerTypesCount == 1)
            {
                return CanAccess(permissions, _customerTypes.First());
            }
            else if (customerTypesCount > 1)
            {
                return CanAccess(permissions, _customerTypes);
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

        private bool IsNeedToCheck()
            => !_customerTypes.Any(m => m.IsInternal);
    }
}
