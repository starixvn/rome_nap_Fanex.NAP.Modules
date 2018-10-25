namespace Fanex.Tarus.Admin.Controller
{
    using Business.MaxMemberLimitBySuper;
    using Criteria.MaxMemberLimitBySuper;
    using Entities.MaxMemberLimitBySuper;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;
    using Sunplus.Accounting.Core.Common;
    using System.Web.Mvc;

    public class MaxNewMemberBySuperController : BaseController
    {
        private IMaxMemberLimitBySuperService _service;

        public MaxNewMemberBySuperController() : this(null)
        {
        }

        public MaxNewMemberBySuperController(IMaxMemberLimitBySuperService service)
        {
            _service = service ?? new MaxMemberLimitBySuperService();
        }

        [AuthenticateRead]
        [HttpGet]
        public ActionResult Index()
        {
            MaxMemberLimitViewModel model = new MaxMemberLimitViewModel()
            {
                ListSites = _service.GetCreditSites(new GetSitesCriteria()
                {
                    System = Constants.System.Accounting,
                    UserId = AppSession.AccId
                })
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
