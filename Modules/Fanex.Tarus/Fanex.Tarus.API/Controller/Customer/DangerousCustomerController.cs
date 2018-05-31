namespace Fanex.Tarus.API.Controller.Customer
{
    using Authentication;
    using Business.Implement.Customer;
    using Business.Interface.Customer;
    using Logging;
    using Model.Customer.DangerousCustomer;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    /// <summary>
    /// Dangerous Customer Controller
    /// Do not inherit ApiController because it's need NewtonsoftJson 4.5.0.0 while NAP cannot running on.
    /// This limitation will cause implement Api like a normal MVC's Controller.
    /// </summary>
    [ApiAuthorization]
    public class DangerousCustomerController : Controller
    {
        private IDangerousCustomerService _dangerousCustomerService;
        
        public DangerousCustomerController()
            : this(null)
        { }

        public DangerousCustomerController(IDangerousCustomerService dangerousCustomerService)
        {
            _dangerousCustomerService = dangerousCustomerService ?? new DangerousCustomerService();
        }

        [HttpPost]
        public string UpdateDangerousCustomer(UpdateDangerousCustomerRequest request)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            LogService.Info("Update Dangerous Customer - Start - Inputs: " + serializer.Serialize(request));
            var result = serializer.Serialize(_dangerousCustomerService.UpdateDangerousCustomer(request));
            LogService.Info("Update Dangerous Customer - End - Outputs: " + result);
            return result;
        }
    }
}
