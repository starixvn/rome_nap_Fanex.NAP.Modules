namespace Fanex.Tarus.Security.Services
{
    using System.Collections.Generic;
    using Models;

    internal interface ICustomerTypeService
    {
        CustomerType GetCustomerType(int custId);

        CustomerType GetCustomerType(string userName);

        IEnumerable<CustomerType> GetCustomerTypes(string userNameList);
    }
}
