using System.Collections.Generic;
using System.Linq;
using Fanex.Tarus.Security.Criteria;
using Fanex.Tarus.Security.Models;
using Fanex.Tarus.Security.Repositories;

namespace Fanex.Tarus.Security.Services
{
    internal interface ICustomerAuthorizationService
    {
        IEnumerable<CustomerSite> GetCustomerSites(string custIdOrUserNameList);

        IEnumerable<CustomerType> GetCustomerTypes(string custIdOrUserNameList);
    }

    internal class CustomerAuthorizationService : ICustomerAuthorizationService
    {
        private readonly ICustomerSiteDataAccess customerSiteDataAccess;
        private readonly ICustomerTypeDataAccess customerTypeDataAccess;
        private readonly IDetectCustomerService detectCustomerService;

        internal CustomerAuthorizationService()
            : this(null, null, null)
        {
        }

        internal CustomerAuthorizationService(
            ICustomerSiteDataAccess customerSiteDataAccess,
            ICustomerTypeDataAccess customerTypeDataAccess,
            IDetectCustomerService detectCustomerService)
        {
            this.customerSiteDataAccess = customerSiteDataAccess ?? new CustomerSiteDataAccess();
            this.customerTypeDataAccess = customerTypeDataAccess ?? new CustomerTypeDataAccess();
            this.detectCustomerService = detectCustomerService ?? new DetectCustomerService();
        }

        public IEnumerable<CustomerSite> GetCustomerSites(string custIdOrUserNameList)
        {
            int custId = detectCustomerService.GetCustId(custIdOrUserNameList);

            var customers = custId > 0
                ? customerSiteDataAccess.GetCustomerSites(new GetCustomerSiteCriteria(custId))
                : customerSiteDataAccess.GetCustomerSites(new GetCustomerSiteCriteria(custIdOrUserNameList));

            return customers.Where(c => c.IsValid && !string.IsNullOrWhiteSpace(c.Site));
        }

        public IEnumerable<CustomerType> GetCustomerTypes(string custIdOrUserNameList)
        {
            int custId = detectCustomerService.GetCustId(custIdOrUserNameList);

            var customerTypes = custId > 0
                ? customerTypeDataAccess.GetCustomerTypes(new GetCustomerTypeCriteria(custId))
                : customerTypeDataAccess.GetCustomerTypes(new GetCustomerTypeCriteria(custIdOrUserNameList));

            return customerTypes.Where(c => c.IsValid);
        }
    }
}
