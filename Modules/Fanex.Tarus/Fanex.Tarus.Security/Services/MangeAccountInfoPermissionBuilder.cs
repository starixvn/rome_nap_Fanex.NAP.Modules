namespace Fanex.Tarus.Security.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria;
    using Repositories;

    public static class MangeAccountInfoPermissionBuilder
    {
        private static readonly string[] ManageAccountInfoPermissions = new[]
        {
            ManageAccountInfoConst.CreditSiteFunctionName,
            ManageAccountInfoConst.DepositSiteFunctionName,
            ManageAccountInfoConst.AccessRealCurrency
        };

        private static readonly string[] CustomerPermissions = new[]
        {
            ManageAccountInfoConst.CreditSiteFunctionName,
            ManageAccountInfoConst.DepositSiteFunctionName,
            ManageAccountInfoConst.SmartPunter
        };

        public static IEnumerable<int> MapToPermissionParameter(int userId, byte policy)
        {
            var permissions = GetManageAccountInfoPermissions(userId, policy);

            for (int i = 0, max = ManageAccountInfoPermissions.Length; i < max; i++)
            {
                if (permissions.ContainsKey(ManageAccountInfoPermissions[i].ToUpperInvariant()))
                {
                    yield return (i + 1);
                }
            }
        }

        public static IDictionary<string, byte> GetManageAccountInfoPermissions(int userId, byte policy)
            => PermissionDataAccess
                .GetUserPermissions(new GetUserPermissionCriteria(userId))
                .Where(f => f.ActualPolicy >= policy && ManageAccountInfoPermissions.Contains(f.FuncName, StringComparer.InvariantCultureIgnoreCase))
                .ToDictionary(kvp => kvp.FuncName.ToUpperInvariant(), kvp => kvp.ActualPolicy);

        public static IDictionary<string, byte> GetCustomerPermissions(int userId, byte policy)
            => PermissionDataAccess
                .GetUserPermissions(new GetUserPermissionCriteria(userId))
                .Where(f => f.ActualPolicy >= policy && CustomerPermissions.Contains(f.FuncName, StringComparer.InvariantCultureIgnoreCase))
                .ToDictionary(kvp => kvp.FuncName.ToUpperInvariant(), kvp => kvp.ActualPolicy);

        private static IUserPermissionDataAccess PermissionDataAccess
            => new UserPermissionDataAccess();
    }
}
