namespace Fanex.Tarus.Security.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Criteria;
    using Models;
    using Repositories;

    internal sealed class CustomerTypeService : ICustomerTypeService
    {
        private readonly ICustomerTypeDataAccess customerTypeDataAccess;

        public CustomerTypeService(ICustomerTypeDataAccess customerTypeDataAccess = null)
        {
            this.customerTypeDataAccess = customerTypeDataAccess ?? new CustomerTypeDataAccess();
        }

        public CustomerType GetCustomerType(int custId)
            => customerTypeDataAccess
                .GetCustomerTypes(new GetCustomerTypeCriteria(custId))
                .FirstOrDefault();

        public CustomerType GetCustomerType(string userName)
            => customerTypeDataAccess
                .GetCustomerTypes(new GetCustomerTypeCriteria(HttpUtility.UrlDecode(userName)))
                .FirstOrDefault();

        public IEnumerable<CustomerType> GetCustomerTypes(string userNameList)
            => customerTypeDataAccess.GetCustomerTypes(new GetCustomerTypeCriteria(userNameList));
    }
}
