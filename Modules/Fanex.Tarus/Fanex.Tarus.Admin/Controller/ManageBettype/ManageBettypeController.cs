namespace Fanex.Tarus.Admin.Controller.ManageBettype
{
    using Fanex.Tarus.Admin.Business.ManageBettype;
    using Fanex.Tarus.Admin.Criteria.ManageBettype;
    using Fanex.Tarus.Admin.DataAccess.ManageBettype;
    using Fanex.Tarus.Admin.Request.ManageBettype;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;
    using System.Web;
    using System.Web.Mvc;

    public class ManageBettypeController : BaseController
    {
        private readonly ManageBettypeService _manageBettypeService;
        private readonly string indexPath = "~/Views/Admin/ManageBettype/Index.cshtml";

        public ManageBettypeController() : this(null)
        { }

        public ManageBettypeController(ManageBettypeService manageBettypeService)
        {
            _manageBettypeService = manageBettypeService ?? new ManageBettypeService();
        }

        [AuthenticateRead]
        [HttpGet]
        public ActionResult Index()
        {
            return View(indexPath);
        }

        [AuthenticateRead]
        [HttpGet]
        public JsonResult Select(SelectRequest request) => Json(_manageBettypeService.Select(new SelectBettypeCriteria {
            TypeId = request.TypeId,
            TypeNamee = request.TypeName,
            PageIndex =  request.PageIndex,
            PageSize = request.PageSize
        }), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult Insert(HttpPostedFileBase uploadFile) => Json(_manageBettypeService.Insert(uploadFile));

        [AuthenticateHost]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Update(UpdateRequest request) => Json(_manageBettypeService.Update(request));
    }
}
