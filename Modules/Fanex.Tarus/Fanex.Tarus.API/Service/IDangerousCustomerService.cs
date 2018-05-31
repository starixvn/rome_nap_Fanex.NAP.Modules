namespace Fanex.Tarus.Business.Interface.Customer
{
    using API.Model.Customer.DangerousCustomer;
    using API.Model.DangerousCustomer;
    using System.Collections.Generic;

    public interface IDangerousCustomerService
    {
        List<UpdateDangerousCustomerResponse> UpdateDangerousCustomer(UpdateDangerousCustomerRequest request);
    }
}
