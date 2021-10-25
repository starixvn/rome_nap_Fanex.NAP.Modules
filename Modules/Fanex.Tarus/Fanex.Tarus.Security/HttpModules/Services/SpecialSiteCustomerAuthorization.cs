namespace Fanex.Tarus.Security.HttpModules.Services
{
    using Criteria;
    using Models;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class SpecialSiteCustomerAuthorization : ICustomerAuthorization
    {
        private readonly IUserPermissionDataAccess permissionDataAccess;
        private readonly IEnumerable<CustomerType> customerTypes;

        internal SpecialSiteCustomerAuthorization() : this(null, null)
        { }

        internal SpecialSiteCustomerAuthorization(IEnumerable<CustomerType> customerTypes) : this(null, customerTypes)
        { }

        internal SpecialSiteCustomerAuthorization(IUserPermissionDataAccess permissionDataAccess, IEnumerable<CustomerType> customerTypes)
        {
            this.permissionDataAccess = permissionDataAccess ?? new UserPermissionDataAccess();
            this.customerTypes = customerTypes;
        }

        public AuthorizationResult CanRead(string parameterValue, int userId)
            => CheckSpecialSitePermission(parameterValue, userId);

        public AuthorizationResult CanHost(string parameterValue, int userId)
            => CheckSpecialSitePermission(parameterValue, userId);

        private AuthorizationResult CheckSpecialSitePermission(string parameterValue, int userId)
        {
            if (!IsNeedToCheck(this.customerTypes))
            {
                return AuthorizationResult.Ok();
            }

            int custId;

            if (int.TryParse(parameterValue, out custId))
            {
                return CheckSpecialSitePermissionByCustId(custId, userId);
            }

            if (parameterValue.IndexOf(',') == -1)
            {
                return CheckSpecialSitePermissionByUserName(parameterValue, userId);
            }

            return CheckSpecialSitePermissionByUserNameList(parameterValue, userId);
        }

        private AuthorizationResult CheckSpecialSitePermissionByCustId(int custId, int userId)
            => permissionDataAccess.CheckSpecialSitePermission(
                new CheckSpecialSitePermissionCriteria(userId, custId, null));

        private AuthorizationResult CheckSpecialSitePermissionByUserName(string userName, int userId)
            => permissionDataAccess.CheckSpecialSitePermission(
                new CheckSpecialSitePermissionCriteria(userId, null, userName));

        private AuthorizationResult CheckSpecialSitePermissionByUserNameList(string userNameList, int userId)
        {
            AuthorizationResult result;
            var usernames = SplitByComma(userNameList);

            foreach (var userName in usernames)
            {
                result = CheckSpecialSitePermissionByUserName(userName, userId);

                if (!result.IsOk)
                {
                    return result;
                }
            }

            return AuthorizationResult.Ok();
        }

        private string[] SplitByComma(string value)
            => string.IsNullOrWhiteSpace(value)
                ? new string[0]
                : value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        /// <summary>
        /// Is Need to check Special Site Permission
        /// </summary>
        /// <param name="customerTypes"> null mean force to check Special Site permission regardless IsInternal or not</param>
        /// <returns></returns>
        private bool IsNeedToCheck(IEnumerable<CustomerType> customerTypes)
            => customerTypes == null || !customerTypes.Any(m => m.IsInternal);
    }
}
