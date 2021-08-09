namespace Fanex.Tarus.Admin.Controller
{
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Tarus.Admin.Business.HeadlineExternalGroupMapping;
    using Fanex.Tarus.Admin.Business.SMAManagement;
    using Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping;
    using Fanex.Tarus.Admin.Entities.HeadlineExternalGroupMapping;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;

    public class HeadlineExternalGroupMappingController : BaseController
    {
        private readonly string indexPath = "~/Views/Admin/HeadlineExternalGroupMapping/Index.cshtml";

        private readonly ISMAManagementService _sMAManagementService;

        private readonly IHeadlineExternalGroupMappingService _headlineExternalGroupMappingService;

        public HeadlineExternalGroupMappingController()
            : this(null, null)
        {
        }

        public HeadlineExternalGroupMappingController(SMAManagementService sMAManagementService, HeadlineExternalGroupMappingService headlineExternalGroupMappingService)
        {
            _sMAManagementService = sMAManagementService ?? new SMAManagementService();
            _headlineExternalGroupMappingService = headlineExternalGroupMappingService ?? new HeadlineExternalGroupMappingService();
        }


        [AuthenticateRead]
        [HttpGet]
        public ActionResult Index()
        {
            return View(indexPath, GetFilterModel());
        }

        [NonAction]
        public FilterModel GetFilterModel()
        {
            var sites = _sMAManagementService.GetSites().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            var currencies = _headlineExternalGroupMappingService.GetCurrencies().ToList();

            var externalGroups = _headlineExternalGroupMappingService.GetExternalGroups().ToList();

            var primaryGroups = _headlineExternalGroupMappingService.GetPrimaryGroups().ToList();

            var headlines = _headlineExternalGroupMappingService.GetHeadline().ToList();

            return new FilterModel
            {
                Sites = sites,
                Currencies = currencies,
                ExternalGroups = externalGroups,
                PrimaryGroups = primaryGroups,
                Headlines = headlines
            };
        }

        [AuthenticateRead]
        public JsonResult GetHeadlines()
            => Json(_headlineExternalGroupMappingService.GetHeadline(), JsonRequestBehavior.AllowGet);

        [AuthenticateRead]
        public JsonResult GetExternalGroups()
            => Json(_headlineExternalGroupMappingService.GetExternalGroups(), JsonRequestBehavior.AllowGet);

        [AuthenticateRead]
        public JsonResult GetPrimaryGroups()
            => Json(_headlineExternalGroupMappingService.GetPrimaryGroups(), JsonRequestBehavior.AllowGet);

        [AuthenticateRead]
        public JsonResult Select(SelExternalGroupHeadlineMappingCriteia request)
            => Json(_headlineExternalGroupMappingService.Select(request), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        public JsonResult Insert(HeadlineExternalGroupMappingModel request)
            => Json(_headlineExternalGroupMappingService.Insert(request, AppSession.AccId));

        [AuthenticateHost]
        public JsonResult Update(HeadlineExternalGroupMappingModel request)
            => Json(_headlineExternalGroupMappingService.Update(request, AppSession.AccId));


        [AuthenticateHost]
        public JsonResult Delete(HeadlineExternalGroupMappingModel request)
            => Json(_headlineExternalGroupMappingService.Delete(request, AppSession.AccId));

        [HttpGet]
        public JsonResult GetExternalGroupHeadlineMappingSuper(string usernames, int groupId)
            => Json(_headlineExternalGroupMappingService.GetMappingSuper(usernames, groupId), JsonRequestBehavior.AllowGet);
    }
}
