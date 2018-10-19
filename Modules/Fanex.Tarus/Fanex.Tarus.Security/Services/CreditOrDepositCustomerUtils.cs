namespace Fanex.Tarus.Security.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Criteria;
    using Repositories;
    using static Sunplus.Accounting.Core.Common.Enumerations;

    public static class CreditOrDepositCustomerUtils
    {
        private const byte CreditAndDepositPermission = 0;
        private const byte CreditPermission = 1;
        private const byte DepositPermission = 2;
        private const byte NoCreditAndDepositPermission = byte.MaxValue;

        public static bool IsNoneCreditAndDepositPermission(byte siteType)
            => siteType == NoCreditAndDepositPermission;

        public static byte GetSiteTypeParamValue(int userId)
        {
            var permissionDictionary = GetCreditAndDepositPolicy(userId);

            if (permissionDictionary.Count != 0)
            {
                var hasCreditSitePermission = permissionDictionary.ContainsKey(Functions.CreditSiteFuncName);
                var hasDepositSitePermission = permissionDictionary.ContainsKey(Functions.DepositSiteFuncName);

                if (hasCreditSitePermission && hasDepositSitePermission)
                {
                    return CreditAndDepositPermission;
                }

                if (hasCreditSitePermission)
                {
                    return CreditPermission;
                }

                if (hasDepositSitePermission)
                {
                    return DepositPermission;
                }
            }

            return NoCreditAndDepositPermission;
        }

        private static IDictionary<string, byte> GetCreditAndDepositPolicy(int userId)
            => PermissionDataAccess.GetUserPermissions(new GetUserPermissionCriteria(userId))
                .Where(f => f.FuncName.Equals(Functions.CreditSiteFuncName, StringComparison.OrdinalIgnoreCase)
                    || f.FuncName.Equals(Functions.DepositSiteFuncName, StringComparison.OrdinalIgnoreCase))
                .ToDictionary(f => f.FuncName.ToLowerInvariant(), f => f.ActualPolicy);

        private static IUserPermissionDataAccess PermissionDataAccess
               => new UserPermissionDataAccess();
    }
}
