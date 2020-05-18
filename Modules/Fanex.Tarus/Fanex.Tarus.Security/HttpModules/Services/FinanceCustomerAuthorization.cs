namespace Fanex.Tarus.Security.HttpModules.Services
{
    using Fanex.Tarus.Security.Criteria.FinanceAuthorization;
    using Fanex.Tarus.Security.Models;
    using Fanex.Tarus.Security.Services;
    using Sunplus.Accounting.Core.Extensions;
    using System;

    public class FinanceCustomerAuthorization : ICustomerAuthorization
    {
        private readonly IFinanceAuthorizationService _financeAuthorizationService;

        public FinanceCustomerAuthorization() : this(null)
        { }

        public FinanceCustomerAuthorization(IFinanceAuthorizationService financeAuthorizationService)
        {
            _financeAuthorizationService = financeAuthorizationService ?? new FinanceAuthorizationService();
        }

        public AuthorizationResult CanRead(string parameterValue, int userId)
        {
            if(_financeAuthorizationService.HasPermission(new CheckFinancePermissionCriteria() { UserName = parameterValue, CheckCustId = true, UserID = userId }))
            {
                return AuthorizationResult.Ok();
            }
            else
            {
                return AuthorizationResult.Forbidden(ResourceExtension.GetResourceValue(ResourceExtension.Resources.Language, "FinanceAccessDenied"));
            }
        }

        public AuthorizationResult CanHost(string parameterValue, int userId) => CanRead(parameterValue, userId);
    }
}
