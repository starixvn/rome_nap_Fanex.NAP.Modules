namespace Fanex.Tarus.Security.HttpModules.Services
{
    using Fanex.Tarus.Security.Constants;
    using Fanex.Tarus.Security.Criteria.FinanceAuthorization;
    using Fanex.Tarus.Security.Models;
    using Fanex.Tarus.Security.Services;
    using System.Collections.Generic;
    using System.Linq;

    public class FinanceCustomerAuthorization : ICustomerAuthorization
    {
        private readonly IFinanceAuthorizationService _financeAuthorizationService;
        private readonly IEnumerable<CustomerType> _customerTypes;

        internal FinanceCustomerAuthorization() : this(null, null)
        { }

        internal FinanceCustomerAuthorization(IEnumerable<CustomerType> customerTypes) : this(null, customerTypes)
        { }

        internal FinanceCustomerAuthorization(IFinanceAuthorizationService financeAuthorizationService, IEnumerable<CustomerType> customerTypes)
        {
            _financeAuthorizationService = financeAuthorizationService ?? new FinanceAuthorizationService();
            this._customerTypes = customerTypes;
        }

        public AuthorizationResult CanRead(string parameterValue, int userId)
        {
            if (!IsNeedToCheck())
            {
                return AuthorizationResult.Ok();
            }

            var permissions = MangeAccountInfoPermissionBuilder.GetManageAccountInfoPermissions(userId, PolicyConst.Read);
            if (IsRealCurrency() && !permissions.ContainsKey(ManageAccountInfoConst.AccessRealCurrency))
            {
                return AuthorizationResult.Forbidden();
            }

            if (!PermissionService.HasPermission(ManageAccountInfoConst.ViewFinanceAccount))
                return AuthorizationResult.Forbidden();

            return _financeAuthorizationService.HasPermission(new CheckFinancePermissionCriteria() { UserName = parameterValue, CheckCustId = true, UserID = userId }) ? AuthorizationResult.Ok() : AuthorizationResult.Forbidden();
        }

        public AuthorizationResult CanHost(string parameterValue, int userId) => CanRead(parameterValue, userId);

        private bool IsNeedToCheck()
            => _customerTypes.Any(m => m.IsInternal);

        private bool IsRealCurrency()
            => _customerTypes.Any(m => m.IsRealCurrency);
    }
}