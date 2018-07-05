namespace Fanex.Tarus.Security.HttpModules.Services
{
    using Criteria;
    using DataAccess;

    internal sealed class CustomerTypeService : ICustomerTypeService
    {
        private readonly CustomerTypeDataAccess customerTypeDataAccess;

        public CustomerTypeService(CustomerTypeDataAccess customerTypeDataAccess = null)
        {
            this.customerTypeDataAccess = customerTypeDataAccess ?? new CustomerTypeDataAccess();
        }

        public bool IsDepositByParam(string custIdOrUserName)
        {
            int custId;

            return int.TryParse(custIdOrUserName, out custId)
                ? IsDeposit(custId)
                : IsDeposit(custIdOrUserName);
        }

        public bool IsDeposit(int custId)
            => customerTypeDataAccess.GetCustomerTypes(new CheckCustomerTypeCriteria(custId));

        public bool IsDeposit(string userName)
            => customerTypeDataAccess.GetCustomerTypes(new CheckCustomerTypeCriteria(userName));
    }
}
