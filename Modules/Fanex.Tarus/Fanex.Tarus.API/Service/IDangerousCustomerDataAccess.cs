namespace Fanex.Tarus.DataAccess.Interface.Customer
{
    using API.Model.Customer.DangerousCustomer;
    using API.Model.DangerousCustomer;
    using System.Collections.Generic;

    public interface IDangerousCustomerDataAccess
    {
        List<UpdateDangerousCustomerResponse> UpdateDangerousCustomer(UpdateDangerousCustomerRequest criteria);
    }
}
