namespace Fanex.Tarus.Admin.Controller
{
    using Fanex.Tarus.Admin.Business.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;
    using System.Web.Mvc;

    public class FinanceGroupsConfigController : BaseController
    {
        private readonly string indexPath = "~/Views/Admin/FinanceGroupsConfig/Index.cshtml";

        private readonly IFinanceGroupsConfigService _financeGroupsConfigService;

        public FinanceGroupsConfigController()
            : this(null)
        {
        }

        public FinanceGroupsConfigController(FinanceGroupsConfigService financeGroupsConfigService) 
        {
            _financeGroupsConfigService = financeGroupsConfigService ?? new FinanceGroupsConfigService();
        }

        [AuthenticateRead]
        public ActionResult Index()
        {
            return View(indexPath);
        }

        [AuthenticateRead]
        [HttpGet]
        public JsonResult GetGroups()
            => Json(_financeGroupsConfigService.GetGroups(), JsonRequestBehavior.AllowGet);

        [AuthenticateRead]
        [HttpPost]
        public JsonResult Select(FinanceGroupSelCriteria request)
            => Json(_financeGroupsConfigService.Select(request), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult UpdateIsView(FinanceGroupIsViewUpdCriteria request)
            => Json(_financeGroupsConfigService.UpdateIsView(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult Update(FinanceGroupUpdCriteria request)
            => Json(_financeGroupsConfigService.Update(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult Remove(FinanceGroupDelCriteria request)
            => Json(_financeGroupsConfigService.Remove(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult RemoveAll(FinanceGroupDeleteAllRequest request)
            => Json(_financeGroupsConfigService.RemoveAll(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult ValidateGroupInsertion(FinanceGroupInsValidationCriteria request)
            => Json(_financeGroupsConfigService.ValidateGroupInsertion(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertGroup(FinanceGroupInsCriteria request)
            => Json(_financeGroupsConfigService.InsertGroup(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult ValidateUserInsertion(FinanceUserInsValidationCriteria request)
            => Json(_financeGroupsConfigService.ValidateUserInsertion(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertUser(FinanceUserInsCriteria request)
            => Json(_financeGroupsConfigService.InsertUser(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult ValidateCodeInsertion(FinanceCodeInsValidationCriteria request)
            => Json(_financeGroupsConfigService.ValidateCodeInsertion(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertCode(FinanceCodeInsCriteria request)
           => Json(_financeGroupsConfigService.InsertCode(request));
    }
}
