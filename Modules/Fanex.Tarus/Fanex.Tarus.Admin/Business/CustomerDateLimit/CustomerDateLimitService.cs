namespace Fanex.Tarus.Admin.Business.CustomerDateLimit
{
    using System.Collections.Generic;
    using Fanex.Tarus.Admin.Criteria.CustomerDateLimit;
    using Fanex.Tarus.Admin.DataAccess.CustomerDateLimit;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.CustomerDateLimit;

    public interface ICustomerDateLimitService
    {
        IEnumerable<CustomerDateLimitModel> GetCustomers(GetAllCustomerDateLimitCriteria criteria);

        CustomerDateLimitModel GetById(int custId);

        CustomerDataRangeResult Update(UpdateCustomerDateLimitCriteria criteria);
    }

    public class CustomerDateLimitService : ICustomerDateLimitService
    {
        private readonly ICustomerDateLimitDataAccess dataAccess;

        public CustomerDateLimitService() : this(null)
        {

        }

        public CustomerDateLimitService(ICustomerDateLimitDataAccess dataAccess)
        {
            this.dataAccess = dataAccess ?? new CustomerDateLimitDataAccess();
        }

        public IEnumerable<CustomerDateLimitModel> GetCustomers(GetAllCustomerDateLimitCriteria criteria)
            => dataAccess.GetCustomers(criteria);

        public CustomerDateLimitModel GetById(int custId)
           => dataAccess.GetById(custId);

        public CustomerDataRangeResult Update(UpdateCustomerDateLimitCriteria criteria)
            => dataAccess.Update(criteria);
    }
}
