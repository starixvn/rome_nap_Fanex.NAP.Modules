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

        internal CustomerAuthorizationService()
            : this(null, null)
        {
        }

        internal CustomerAuthorizationService(
            ICustomerSiteDataAccess customerSiteDataAccess,
            ICustomerTypeDataAccess customerTypeDataAccess)
        {
            this.customerSiteDataAccess = customerSiteDataAccess ?? new CustomerSiteDataAccess();
            this.customerTypeDataAccess = customerTypeDataAccess ?? new CustomerTypeDataAccess();
        }

        public IEnumerable<CustomerSite> GetCustomerSites(string custIdOrUserNameList)
        {
            int custId;

            var customers = int.TryParse(custIdOrUserNameList, out custId)
                ? customerSiteDataAccess.GetCustomerSites(new GetCustomerSiteCriteria(custId))
                : customerSiteDataAccess.GetCustomerSites(new GetCustomerSiteCriteria(custIdOrUserNameList));

            return customers.Where(c => c.IsValid && !string.IsNullOrWhiteSpace(c.Site));
        }

        public IEnumerable<CustomerType> GetCustomerTypes(string custIdOrUserNameList)
        {
            int custId;

            var customerTypes = int.TryParse(custIdOrUserNameList, out custId)
                ? customerTypeDataAccess.GetCustomerTypes(new GetCustomerTypeCriteria(custId))
                : customerTypeDataAccess.GetCustomerTypes(new GetCustomerTypeCriteria(custIdOrUserNameList));

            return customerTypes.Where(c => c.IsValid);
        }
    }
}
