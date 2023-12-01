namespace Fanex.Tarus.Security.HttpModules.Services
{
    using Fanex.Tarus.Security.Criteria.FinanceAuthorization;
    using Fanex.Tarus.Security.Models;
    using Fanex.Tarus.Security.Services;
    using Sunplus.Accounting.Core.Common;
    using Sunplus.Accounting.Core.Extensions;
    using System.Collections.Generic;
    using System.Linq;

    public class FinanceCustomerAuthorization : ICustomerAuthorization
    {
        private readonly IFinanceAuthorizationService _financeAuthorizationService;
        private readonly string _viewFinanceAccountFunction = Enumerations.Functions.GetName(Enumerations.Functions.FinancialManagement.ViewFinanceAccount);
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

            if (PermissionService.HasPermission(_viewFinanceAccountFunction))
            {
                if (_financeAuthorizationService.HasPermission(new CheckFinancePermissionCriteria() { UserName = parameterValue, CheckCustId = true, UserID = userId }))
                {
                    return AuthorizationResult.Ok();
                }
            }

            return AuthorizationResult.Forbidden(ResourceExtension.GetResourceValue(ResourceExtension.Resources.Language, "FinanceAccessDenied"));
        }

        public AuthorizationResult CanHost(string parameterValue, int userId) => CanRead(parameterValue, userId);

        private bool IsNeedToCheck()
            => _customerTypes.Any(m => m.IsInternal);
    }
}