namespace Fanex.Tarus.Admin.Controller
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Fanex.Tarus.Admin.Business.SMAManagement;
    using Fanex.Tarus.Admin.Entities.SMAManagement;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageBettype;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageCSSportBettypeMatrix;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageProduct;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeMatrix;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSporttype;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageBettype;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageCSSportBettypeMatrix;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageProduct;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageRefResource;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageSportBettypeMatrix;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageSportType;
    using Fanex.Tarus.Admin.ManageProductSetting.Services;
    using Fanex.Tarus.Admin.ManageProductSetting.ViewModel;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;
    using System;

    public class ManageProductSettingController : BaseController
    {
        private readonly string indexPath = "~/Views/Admin/ManageProductSetting/Index.cshtml";
        private readonly ManageProductService _manageProductService;
        private readonly ManageSportTypeService _manageSportTypeService;
        private readonly ManageBettypeService _manageBettypeService;
        private readonly ManageSportBettypeMatrixService _manageSportBettypeMatrixService;
        private readonly ManageCanSeeService _manageCanSeeService;
        private readonly SMAManagementService _SMAManagementService;
        private readonly IManageCSSportBettypeMatrixService _manageCSSportBettypeMatrixService;
        private readonly IManageRefResourceService _manageRefResourceService;

        public ManageProductSettingController()
        {
            _manageProductService = new ManageProductService();
            _manageSportTypeService = new ManageSportTypeService();
            _manageBettypeService = new ManageBettypeService();
            _manageSportBettypeMatrixService = new ManageSportBettypeMatrixService();
            _manageCanSeeService = new ManageCanSeeService();
            _SMAManagementService = new SMAManagementService();
            _manageCSSportBettypeMatrixService = new ManageCSSportBettypeMatrixService();
            _manageRefResourceService = new ManageRefResourceService();
        }

        [AuthenticateRead]
        [HttpGet]
        public ActionResult Index()
        {
            var site = _SMAManagementService.GetSites(1).ToList();
            site.Insert(0, new IdNameEntity { Id = 0, Name = "All Sites" });
            var currency = _manageCanSeeService.GetCurrencies();
            var sports = _manageCanSeeService.GetAllSportType();
            var viewModel = new ManageProductSettingViewModel
            {
                Products = _manageSportBettypeMatrixService.GetListProducts(),
                Sites = site.Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }),
                Currencies = currency.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                SportTypes = sports.Select(s => new SelectListItem
                {
                    Text = $"[{s.Key.ToString()}] {s.Value}",
                    Value = s.Key.ToString()
                })
            };
            
            ViewBag.BetTypeGroups = _manageCSSportBettypeMatrixService.GetBetTypeGroups();
            ViewBag.SportTypeGroups = _manageCSSportBettypeMatrixService.GetSportTypeGroups();

            return View(indexPath, viewModel);
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
            SportName = request.SportName,
            BetType = request.BetType,
            BetTypeName = request.BetTypeName,
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

        [AuthenticateHost]
        [HttpPost]
        public JsonResult DeleteSportBettypeMatrix(DeleteSportBettypeMatrixRequest request) => Json(_manageSportBettypeMatrixService.Delete(request));

        #endregion SportBettypeMatrix

        #region CanSee

        [AuthenticateRead]
        public JsonResult SelectCanSee(SelectCanSeeCriteria request)
            => Json(_manageCanSeeService.Select(request), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertCanSee(HttpPostedFileBase uploadFile) => Json(_manageCanSeeService.Insert(uploadFile));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult DeleteCanSee(DeleteCanSeeCriteria request) => Json(_manageCanSeeService.Delete(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult UpdateCanSee(UpdateCanSeeCriteria request) => Json(_manageCanSeeService.Update(request));

        #endregion CanSee
        
        
        #region CSSportBettypeMatrix
        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectCSSportBettypeMatrix(SelectCSSportBettypeMatrixRequest request) => Json(_manageCSSportBettypeMatrixService.Select(new SelectCSSportBettypeMatrixCriteria
        {
            SportType = request.SportType,
            SportName = request.SportName,
            BetType = request.BetType,
            BetTypeName = request.BetTypeName,
            ProductId = request.ProductId == null ? "" : request.ProductId,
            EventType = request.EventType,
            RefTable = request.RefTable,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        }), JsonRequestBehavior.AllowGet);

        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectSportTypeGroups() => Json(_manageCSSportBettypeMatrixService.GetSportTypeGroups(), JsonRequestBehavior.AllowGet);

        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectBetTypeGroups() => Json(_manageCSSportBettypeMatrixService.GetBetTypeGroups(), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertCSSportBettypeMatrix(HttpPostedFileBase uploadFile) => Json(_manageCSSportBettypeMatrixService.Insert(uploadFile));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult UpdateCSSportBettypeMatrix(UpdateCSSportBettypeMatrixRequest request) => Json(_manageCSSportBettypeMatrixService.Update(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult DeleteCSSportBettypeMatrix(DeleteCSSportBettypeMatrixRequest request) => Json(_manageCSSportBettypeMatrixService.Delete(request));

        #endregion CSSportBettypeMatrix

        #region RefResource
        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectRefResource(SelectRefResourceRequest request) => Json(_manageRefResourceService.Select(request), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertRefResource(HttpPostedFileBase uploadFile) => Json(_manageRefResourceService.Insert(uploadFile));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult UpdateRefResource(UpdateRefResourceRequest request) => Json(_manageRefResourceService.Update(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult DeleteRefResource(DeleteRefResourceRequest request) => Json(_manageRefResourceService.Delete(request));

        #endregion
    }
}