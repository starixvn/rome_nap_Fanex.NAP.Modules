namespace Fanex.Tarus.Admin.Controller
{
    using Criteria.CompareBalance;
    using DataAccess.CompareBalance;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;
    using Sunplus.Accounting.Core.Common;
    using System.Web.Mvc;

    public class CompareBalanceController : BaseController
    {
        private readonly CompareBalanceDataAccess _service;

        public CompareBalanceController() : this(null)
        { }

        public CompareBalanceController(CompareBalanceDataAccess service)
        {
            _service = service ?? new CompareBalanceDataAccess();
        }

        [AuthenticateRead]
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Admin/CompareBalance/Index.cshtml", null);
        }

        [AuthenticateRead]
        [HttpPost]
        public JsonResult Search(GetCompareBalanceCriteria criteria)
        {
            return Json(_service.GetCompareBalance(criteria));
        }
    }
}
