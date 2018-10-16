namespace Fanex.Tarus.Admin.Controller
{
    using System.Web.Mvc;
    using Business.CustomerDateLimit;
    using Criteria.CustomerDateLimit;
    using Entities.CustomerDateLimit;
    using Enums;
    using Infrastructure.Common;
    using Web.Controllers;
    using Web.Controllers.Filters;

    public class CustomerDateLimitController : BaseController
    {
        private readonly ICustomerDateLimitService service;

        public CustomerDateLimitController() : this(null)
        {

        }

        public CustomerDateLimitController(ICustomerDateLimitService service)
        {
            this.service = service ?? new CustomerDateLimitService();
        }

        [AuthenticateRead]
        public ActionResult Index() => View("~/Views/Admin/CustomerDateLimit/Index.cshtml");

        [AuthenticateRead]
        public ActionResult Get(GetAllCustomerDateLimitCriteria criteria)
        {
            var customers = service.GetCustomers(criteria);

            return Json(new CustomerDateLimitViewModel(customers, criteria.TotalRecords, criteria.PageSize, criteria.PageIndex));
        }

        [AuthenticateRead]
        public ActionResult GetById(int custId)
        {
            var customer = service.GetById(custId);

            return View("~/Views/Admin/CustomerDateLimit/Edit.cshtml", customer);
        }

        [AuthenticateHost]
        public ActionResult Add()
        {
            var customer = new CustomerDateLimitModel();

            return View("~/Views/Admin/CustomerDateLimit/Add.cshtml", customer);
        }

        [HttpGet]
        [AuthenticateHost]
        public ActionResult Remove()
        {
            var customer = new CustomerDateLimitModel();

            return View("~/Views/Admin/CustomerDateLimit/Remove.cshtml", customer);
        }

        [HttpPost]
        [AuthenticateHost]
        public ActionResult Remove(UpdateCustomerDateLimitCriteria criteria)
        {
            if (criteria != null)
            {                
                criteria.Choice = CustomerDataRangeType.None;
            }

            return Update(criteria);
        }

        [AuthenticateHost]
        public ActionResult Update(UpdateCustomerDateLimitCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.UserId = AppSession.AccId;
            }

            return Json(service.Update(criteria));
        }
    }
}
