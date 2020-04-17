namespace Fanex.Tarus.Admin.Controller.ManageSporttype
{
    using Fanex.Tarus.Admin.Business.ManageSportType;
    using Fanex.Tarus.Admin.Criteria.ManageSporttype;
    using Fanex.Tarus.Admin.Request.ManageSportType;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;
    using System.Web;
    using System.Web.Mvc;

    public class ManageSportTypeController : BaseController
    {
        private readonly ManageSportTypeService _manageSportTypeService;
        private readonly string indexPath = "~/Views/Admin/ManageSportType/Index.cshtml";

        public ManageSportTypeController() : this(null)
        { }

        public ManageSportTypeController(ManageSportTypeService manageSportTypeService)
        {
            _manageSportTypeService = manageSportTypeService ?? new ManageSportTypeService();
        }

        [AuthenticateRead]
        [HttpGet]
        public ActionResult Index()
        {
            return View(indexPath);
        }

        [AuthenticateRead]
        [HttpGet]
        public JsonResult Select(SelectRequest request) => Json(_manageSportTypeService.Select(new SelectSportTypeCriteria
        {
            SportType = request.SportType,
            SportName = request.SportName,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        }), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult Insert(HttpPostedFileBase uploadFile) => Json(_manageSportTypeService.Insert(uploadFile));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult Update(UpdateRequest request) => Json(_manageSportTypeService.Update(request));
    }
}
