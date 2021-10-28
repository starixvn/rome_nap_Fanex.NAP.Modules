namespace Fanex.Tarus.Security.HttpModules.Services
{
    using Fanex.Tarus.Security.Criteria.FinanceAuthorization;
    using Fanex.Tarus.Security.Models;
    using Fanex.Tarus.Security.Services;
    using Sunplus.Accounting.Core.Common;
    using Sunplus.Accounting.Core.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FinanceCustomerAuthorization : ICustomerAuthorization
    {
        private const string CN88SiteName = "cn88";
        private readonly IFinanceAuthorizationService _financeAuthorizationService;
        private readonly string ViewFinanceAccountFunction = Enumerations.Functions.GetName(Enumerations.Functions.FinancialManagement.ViewFinanceAccount);
        private readonly IEnumerable<CustomerType> customerTypes;
        private readonly IEnumerable<CustomerSite> customerSites;

        internal FinanceCustomerAuthorization() : this(null, null, null)
        { }


        internal FinanceCustomerAuthorization(IEnumerable<CustomerSite> customerSites, IEnumerable<CustomerType> customerTypes) : this(null, customerSites, customerTypes)
        { }

        internal FinanceCustomerAuthorization(IFinanceAuthorizationService financeAuthorizationService, IEnumerable<CustomerSite> customerSites, IEnumerable<CustomerType> customerTypes)
        {
            _financeAuthorizationService = financeAuthorizationService ?? new FinanceAuthorizationService();
            this.customerTypes = customerTypes;
            this.customerSites = customerSites;
        }

        public AuthorizationResult CanRead(string parameterValue, int userId)
        {
            if (!IsNeedToCheck())
            {
                return AuthorizationResult.Ok();
            }

            if (PermissionService.HasPermission(ViewFinanceAccountFunction))
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
            => !customerSites.Any(m => m.Site.Equals(CN88SiteName, StringComparison.OrdinalIgnoreCase)) && customerTypes.Any(m => m.IsInternal);

    }
}
