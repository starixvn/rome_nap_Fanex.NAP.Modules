namespace Fanex.Tarus.Security.HttpModules.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria;
    using Models;
    using Repositories;
    using Security.Services;

    internal sealed class CreditOrDepositCustomerAuthorization : ICustomerAuthorization
    {
        private readonly ICustomerTypeService customerTypeService;
        private readonly IUserPermissionDataAccess permissionDataAccess;

        public CreditOrDepositCustomerAuthorization()
            : this(null, null)
        {
        }

        internal CreditOrDepositCustomerAuthorization(
            ICustomerTypeService customerTypeService,
            IUserPermissionDataAccess permissionDataAccess)
        {
            this.customerTypeService = customerTypeService ?? new CustomerTypeService();
            this.permissionDataAccess = permissionDataAccess ?? new UserPermissionDataAccess();
        }

        public AuthorizationResult CanRead(string parameterValue, int userId)
            => CanAccess(parameterValue, userId, PolicyConst.Read);

        public AuthorizationResult CanHost(string parameterValue, int userId)
            => CanAccess(parameterValue, userId, PolicyConst.Host);

        public AuthorizationResult CanAccess(string parameterValue, int userId, byte urlPolicy)
        {
            if (parameterValue.IndexOf(',') != -1)
            {
                return CanAccessByUserNameList(parameterValue, userId, urlPolicy);
            }

            return CanAccessByCustIdOrUserName(parameterValue, userId, urlPolicy);
        }

        private AuthorizationResult CanAccessByCustIdOrUserName(string custIdOrUserName, int userId, byte urlPolicy)
        {
            int custId;

            var customerType = int.TryParse(custIdOrUserName, out custId)
                ? customerTypeService.GetCustomerType(custId)
                : customerTypeService.GetCustomerType(custIdOrUserName);

            if (customerType == null || !customerType.IsValid)
            {
                return AuthorizationResult.Ok();
            }

            var functionName = customerType.IsDeposit
                ? ManageAccountInfoConst.DepositSiteFunctionName
                : ManageAccountInfoConst.CreditSiteFunctionName;
            var userPolicy = GetActualPolicy(userId, functionName);

            return !userPolicy.HasValue || userPolicy.Value < urlPolicy
                ? AuthorizationResult.Forbidden()
                : AuthorizationResult.Ok();
        }

        private AuthorizationResult CanAccessByUserNameList(string userNameList, int userId, byte urlPolicy)
        {
            ICollection<string> creditCustomers = new List<string>();
            ICollection<string> depositCustomers = new List<string>();
            GetCreditAndDepositCustomers(userNameList, ref creditCustomers, ref depositCustomers);

            var authorizationResult = GetActualPolicy(creditCustomers, userId, ManageAccountInfoConst.CreditSiteFunctionName, urlPolicy);

            if (authorizationResult.IsOk)
            {
                authorizationResult = GetActualPolicy(depositCustomers, userId, ManageAccountInfoConst.DepositSiteFunctionName, urlPolicy);
            }

            return authorizationResult;
        }

        private void GetCreditAndDepositCustomers(
            string userNameList,
            ref ICollection<string> creditCustomers,
            ref ICollection<string> depositCustomers)
        {
            var customers = customerTypeService
                   .GetCustomerTypes(userNameList)
                   .Where(c => c.IsValid);

            foreach (var customer in customers)
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
        }

        private AuthorizationResult GetActualPolicy(ICollection<string> customers, int userId, string functionName, byte urlPolicy)
        {
            if (customers.Count > 0)
            {
                var userPolicy = GetActualPolicy(userId, functionName);

                if (!userPolicy.HasValue || userPolicy.Value < urlPolicy)
                {
                    return AuthorizationResult.Forbidden($"Invalid usernames: {string.Join(", ", customers)}");
                }
            }

            return AuthorizationResult.Ok();
        }

        private byte? GetActualPolicy(int userId, string functionName)
        {
            var userPermission = permissionDataAccess
                .GetUserPermissions(new GetUserPermissionCriteria(userId))
                .FirstOrDefault(f => f.FuncName.Equals(functionName, StringComparison.OrdinalIgnoreCase));

            return userPermission?.ActualPolicy;
        }
    }
}
