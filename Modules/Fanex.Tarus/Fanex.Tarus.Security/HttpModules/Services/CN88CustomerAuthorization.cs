namespace Fanex.Tarus.Security.HttpModules.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria;
    using Models;
    using NAP.Framework.Data;
    using Repositories;

    public sealed class CN88CustomerAuthorization : ICustomerAuthorization
    {
        private readonly ICustomerSiteDataAccess customerSiteDataAccess;
        private readonly IUserPermissionDataAccess permissionDataAccess;

        public CN88CustomerAuthorization()
            : this(null, null)
        {
        }

        internal CN88CustomerAuthorization(
            ICustomerSiteDataAccess customerSiteDataAccess,
            IUserPermissionDataAccess permissionDataAccess)
        {
            this.customerSiteDataAccess = customerSiteDataAccess ?? new CustomerSiteDataAccess();
            this.permissionDataAccess = permissionDataAccess ?? new UserPermissionDataAccess();
        }

        public AuthorizationResult CanRead(string parameterValue, int userId)
            => CanAccess(parameterValue, userId, PolicyConst.Read);

        public AuthorizationResult CanHost(string parameterValue, int userId)
            => CanAccess(parameterValue, userId, PolicyConst.Host);

        public AuthorizationResult CanAccess(string parameterValue, int userId, byte urlPolicy)
            => parameterValue.IndexOf(',') != -1
            ? CanAccessByUserNameList(parameterValue, userId, urlPolicy)
            : CanAccessByCustIdOrUserName(parameterValue, userId, urlPolicy);

        private AuthorizationResult CanAccessByUserNameList(string userNameList, int userId, byte urlPolicy)
        {
            var customers = GetCustomerSites(userNameList)
                .Where(c => c.IsCN88Site)
                .ToList();

            if (customers.Count > 0)
            {
                var userPolicy = GetActualPolicy(userId);

                if (!userPolicy.HasValue || userPolicy.Value < urlPolicy)
                {
                    return AuthorizationResult.Forbidden($"Invalid usernames: {string.Join(", ", customers.Select(c => c.CustInfo))}");
                }
            }

            return AuthorizationResult.Ok();
        }

        private AuthorizationResult CanAccessByCustIdOrUserName(string custIdOrUserName, int userId, byte urlPolicy)
        {
            var customerSite = GetCustomerSite(custIdOrUserName);

            if (customerSite != null && customerSite.IsCN88Site)
            {
                var userPolicy = GetActualPolicy(userId);

                return !userPolicy.HasValue || userPolicy.Value < urlPolicy
                    ? AuthorizationResult.Forbidden()
                    : AuthorizationResult.Ok();
            }

            return AuthorizationResult.Ok();
        }

        private IEnumerable<CustomerSite> GetCustomerSites(string userNameList)
            => customerSiteDataAccess
                .GetCustomerSites(new GetCustomerSiteCriteria(userNameList));

        private CustomerSite GetCustomerSite(string custIdOrUserName)
        {
            int custId;

            ICriteria criteria = int.TryParse(custIdOrUserName, out custId)
                ? new GetCustomerSiteCriteria(custId)
                : new GetCustomerSiteCriteria(custIdOrUserName);

            return customerSiteDataAccess
                .GetCustomerSites(criteria)
                .FirstOrDefault();
        }

        private byte? GetActualPolicy(int userId)
        {
            var userPermission = permissionDataAccess
                .GetUserPermissions(new GetUserPermissionCriteria(userId))
                .FirstOrDefault(f => f.FuncName.Equals(ManageAccountInfoConst.CN88SiteFunctionName, StringComparison.OrdinalIgnoreCase));

            return userPermission?.ActualPolicy;
        }
    }
}
