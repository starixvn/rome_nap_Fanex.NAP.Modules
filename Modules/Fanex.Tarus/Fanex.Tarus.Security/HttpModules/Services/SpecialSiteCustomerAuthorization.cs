namespace Fanex.Tarus.Security.HttpModules.Services
{
    using System;
    using Criteria;
    using Models;
    using Repositories;

    public sealed class SpecialSiteCustomerAuthorization : ICustomerAuthorization
    {
        private readonly IUserPermissionDataAccess permissionDataAccess;

        public SpecialSiteCustomerAuthorization(IUserPermissionDataAccess permissionDataAccess = null)
        {
            this.permissionDataAccess = permissionDataAccess ?? new UserPermissionDataAccess();
        }

        public AuthorizationResult CanRead(string parameterValue, int userId)
            => CheckSpecialSitePermission(parameterValue, userId);

        public AuthorizationResult CanHost(string parameterValue, int userId)
            => CheckSpecialSitePermission(parameterValue, userId);

        private AuthorizationResult CheckSpecialSitePermission(string parameterValue, int userId)
        {
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
    }
}
