namespace Fanex.Tarus.Business.Implement.Customer
{
    using API.Model.Customer.DangerousCustomer;
    using API.Model.DangerousCustomer;
    using DataAccess.Implement.Customer;
    using DataAccess.Interface.Customer;
    using Interface.Customer;
    using System.Collections.Generic;

    public class DangerousCustomerService : IDangerousCustomerService
    {

        private static IDangerousCustomerDataAccess _dangerousCustomerDataAccess;

        public DangerousCustomerService(IDangerousCustomerDataAccess depositCustomerDataAccess)
        {
            _dangerousCustomerDataAccess = depositCustomerDataAccess ?? new DangerousCustomerDataAccess();
        }

        public DangerousCustomerService()
            : this(null)
        { }

        public List<UpdateDangerousCustomerResponse> UpdateDangerousCustomer(UpdateDangerousCustomerRequest request)
        {
            return _dangerousCustomerDataAccess.UpdateDangerousCustomer(request);
        }
    }
}
