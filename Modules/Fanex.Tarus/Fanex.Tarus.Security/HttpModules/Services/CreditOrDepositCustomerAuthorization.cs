namespace Fanex.Tarus.Security.HttpModules.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constant;
    using Constants;
    using Security.Services;

    public sealed class CreditOrDepositCustomerAuthorization : ICreditOrDepositCustomerAuthorization
    {
        private readonly ICustomerTypeService customerTypeService;
        private readonly IUserPermissionService userPermissionService;

        public CreditOrDepositCustomerAuthorization()
            : this(null, null)
        {
        }

        internal CreditOrDepositCustomerAuthorization(
            ICustomerTypeService customerTypeService,
            IUserPermissionService userPermissionService)
        {
            this.customerTypeService = customerTypeService ?? new CustomerTypeService();
            this.userPermissionService = userPermissionService ?? new UserPermissionService();
        }

        public bool CanRead(string custIdOrUserName, int userId)
            => GetPolicy(custIdOrUserName, userId).HasValue;

        public bool CanHost(string custIdOrUserName, int userId)
            => GetPolicy(custIdOrUserName, userId) == PolicyConst.Host;

        public byte? GetPolicy(string custIdOrUserName, int userId)
        {
            var isDepositCustomer = customerTypeService.IsDepositByParam(custIdOrUserName);
            var functionName = isDepositCustomer ? FunctionNameConst.DepositSite : FunctionNameConst.CreditSite;

            return userPermissionService.GetActualPolicy(userId, functionName);
        }

        public IDictionary<string, byte> GetCreditAndDepositPolicy(int userId)
            => userPermissionService.GetUserPermissions(userId).Where(p =>
                    p.FuncName.Equals(FunctionNameConst.CreditSite, StringComparison.OrdinalIgnoreCase) ||
                    p.FuncName.Equals(FunctionNameConst.DepositSite, StringComparison.OrdinalIgnoreCase))
                .ToDictionary(kvp => kvp.FuncName.ToLowerInvariant(), kvp => kvp.ActualPolicy);
    }
}
