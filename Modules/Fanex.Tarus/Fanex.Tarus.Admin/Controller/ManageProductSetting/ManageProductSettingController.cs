using System.Web;
using System.Web.Mvc;
using Fanex.Tarus.Admin.Business.ManageBettype;
using Fanex.Tarus.Admin.Business.ManageProduct;
using Fanex.Tarus.Admin.Business.ManageSportBettypeMatrix;
using Fanex.Tarus.Admin.Business.ManageSportType;
using Fanex.Tarus.Admin.Criteria.ManageBettype;
using Fanex.Tarus.Admin.Criteria.ManageProduct;
using Fanex.Tarus.Admin.Criteria.ManageSportBettypeMatrix;
using Fanex.Tarus.Admin.Criteria.ManageSporttype;
using Fanex.Tarus.Admin.Request.ManageBettype;
using Fanex.Tarus.Admin.Request.ManageProduct;
using Fanex.Tarus.Admin.Request.ManageSportBettypeMatrix;
using Fanex.Tarus.Admin.Request.ManageSportType;
using Fanex.Tarus.Web.Controllers;
using Fanex.Tarus.Web.Controllers.Filters;

namespace Fanex.Tarus.Admin.Controller.ManageProductSetting
{
    public class ManageProductSettingController : BaseController
    {
        private readonly string indexPath = "~/Views/Admin/ManageProductSetting/Index.cshtml";
        private readonly ManageProductService _manageProductService;
        private readonly ManageSportTypeService _manageSportTypeService;
        private readonly ManageBettypeService _manageBettypeService;
        private readonly ManageSportBettypeMatrixService _manageSportBettypeMatrixService;

        public ManageProductSettingController()
        {
            _manageProductService = new ManageProductService();
            _manageSportTypeService = new ManageSportTypeService();
            _manageBettypeService = new ManageBettypeService();
            _manageSportBettypeMatrixService = new ManageSportBettypeMatrixService();
        }

        [AuthenticateRead]
        [HttpGet]
        public ActionResult Index()
        {
            return View(indexPath);
        }

        #region Bettype

        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectBettype(SelectBettypeRequest request) => Json(_manageBettypeService.Select(new SelectBettypeCriteria
        {
            TypeId = request.TypeId,
            TypeNamee = request.TypeName,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        }), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertBettype(HttpPostedFileBase uploadFile) => Json(_manageBettypeService.Insert(uploadFile));

        [AuthenticateHost]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateBettype(UpdateBettypeRequest request) => Json(_manageBettypeService.Update(request));

        #endregion Bettype

        #region SportType

        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectSport(SelectSportRequest request) => Json(_manageSportTypeService.Select(new SelectSportTypeCriteria
        {
            SportType = request.SportType,
            SportName = request.SportName,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        }), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertSport(HttpPostedFileBase uploadFile) => Json(_manageSportTypeService.Insert(uploadFile));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult UpdateSport(UpdateSportRequest request) => Json(_manageSportTypeService.Update(request));

        #endregion SportType

        #region Product

        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectProduct(SelectProductRequest request) => Json(_manageProductService.Select(new SelectProductCriteria
        {
            ProductID = request.ProductID,
            ProductName = request.ProductName,
            ProductPrefix = request.ProductPrefix,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        }), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertProduct(HttpPostedFileBase uploadFile) => Json(_manageProductService.Insert(uploadFile));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult UpdateProduct(UpdateProductRequest request) => Json(_manageProductService.Update(request));

        #endregion Product

        #region SportBettypeMatrix

        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectSportBettypeMatrix(SelectSportBettypeMatrixRequest request) => Json(_manageSportBettypeMatrixService.Select(new SelectSportBettypeMatrixCriteria
        {
            SportType = request.SportType,
            BetType = request.BetType,
            ProductId = request.ProductId,
            EventType = request.EventType,
            RefTable = request.RefTable,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        }), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertSportBettypeMatrix(HttpPostedFileBase uploadFile) => Json(_manageSportBettypeMatrixService.Insert(uploadFile));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult UpdateSportBettypeMatrix(UpdateSportBettypeMatrixRequest request) => Json(_manageSportBettypeMatrixService.Update(request));

        #endregion SportBettypeMatrix
    }
}