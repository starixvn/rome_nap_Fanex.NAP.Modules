namespace Fanex.Tarus.Admin.Controller
{
    using Business.MaxMemberLimitBySuper;
    using Criteria.MaxMemberLimitBySuper;
    using Entities.MaxMemberLimitBySuper;
    using Fanex.Tarus.Admin.Business.SMAManagement;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;
    using Sunplus.Accounting.Core.Common;
    using System.Web.Mvc;

    public class MaxNewMemberBySuperController : BaseController
    {
        private IMaxMemberLimitBySuperService _service;
        readonly ISMAManagementService _smaManagementService;

        public MaxNewMemberBySuperController() : this(null, null)
        {
        }

        public MaxNewMemberBySuperController(IMaxMemberLimitBySuperService service, ISMAManagementService smaManagemenentService)
        {
            _service = service ?? new MaxMemberLimitBySuperService();
            _smaManagementService = smaManagemenentService ?? new SMAManagementService();
        }

        [AuthenticateRead]
        [HttpGet]
        public ActionResult Index()
        {
            var model = new MaxMemberLimitViewModel()
            {
                ListSites = _smaManagementService.GetSites()
            };

            return View("~/Views/Admin/MaxNewMemberBySuper/Index.cshtml", model);
        }

        [AuthenticateRead]
        [HttpPost]
        public JsonResult Search(SearchSuperCriteria criteria) => Json(_service.SearchSuper(criteria));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult Update(UpdateSuperCriteria criteria) => Json(_service.UpdateSuper(criteria));
    }
}
