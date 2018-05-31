namespace Fanex.Tarus.API.Model.Customer.DangerousCustomer
{
    using Fanex.Tarus.API.Model.Customer.DangerousCustomer.Enums;

    public class UpdateDangerousCustomerRequest
    {
        public string ListUserName { get; set; }

        public DangerousType DangerousType { get; set; }

        public DangerousLevel DangerousLevel { get; set; }

        public string UserName { get; set; }

        public string Ip { get; set; }
    }
}
