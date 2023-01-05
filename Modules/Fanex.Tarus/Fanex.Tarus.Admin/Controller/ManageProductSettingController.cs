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
    using Fanex.Tarus.Admin.ManageProductSetting.Entities.UMConfig;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageBettype;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageCSSportBettypeMatrix;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageProduct;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageRefResource;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageResetWinning;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageSportBettypeMatrix;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageSportType;
    using Fanex.Tarus.Admin.ManageProductSetting.Services;
    using Fanex.Tarus.Admin.ManageProductSetting.ViewModel;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;

    public class ManageProductSettingController : BaseController
    {
        private readonly string indexPath = "~/Views/Admin/ManageProductSetting/Index.cshtml";
        private readonly string productPath = "~/Views/Admin/ManageProductSetting/Product/_Product.cshtml";
        private readonly string sportPath = "~/Views/Admin/ManageProductSetting/Sport/_Sport.cshtml";
        private readonly string bettypePath = "~/Views/Admin/ManageProductSetting/Bettype/_Bettype.cshtml";
        private readonly string sportBettypeMatrixPath = "~/Views/Admin/ManageProductSetting/SportBettypeMatrix/_SportBettypeMatrix.cshtml";
        private readonly string csSportBettypeMatrixPath = "~/Views/Admin/ManageProductSetting/CSSportBettypeMatrix/_CSSportBettypeMatrix.cshtml";
        private readonly string refResourcePath = "~/Views/Admin/ManageProductSetting/RefResource/_RefResource.cshtml";
        private readonly string canSeePath = "~/Views/Admin/ManageProductSetting/CanSee/_index.cshtml";
        private readonly string resetWinningPath = "~/Views/Admin/ManageProductSetting/ResetWinning/_ResetWinning.cshtml";
        private readonly string umConfigPath = "~/Views/Admin/ManageProductSetting/UMConfig/_UmConfig.cshtml";
        private readonly string vendorPath = "~/Views/Admin/ManageProductSetting/VendorGlobalSetting/_VendorGlobalSetting.cshtml";
        private readonly string product3rdPath = "~/Views/Admin/ManageProductSetting/Product3rd/_Product3rd.cshtml";
        private readonly ManageProductService _manageProductService;
        private readonly ManageSportTypeService _manageSportTypeService;
        private readonly ManageBettypeService _manageBettypeService;
        private readonly ManageSportBettypeMatrixService _manageSportBettypeMatrixService;
        private readonly ManageCanSeeService _manageCanSeeService;
        private readonly SMAManagementService _SMAManagementService;
        private readonly IManageCSSportBettypeMatrixService _manageCSSportBettypeMatrixService;
        private readonly IManageRefResourceService _manageRefResourceService;
        private readonly IManageDwLListService _manageDwLListService;
        private readonly IManageUMConfigService _manageUMConfigService;
        private readonly IManageDwLListExtraPTService _manageDwLListExtraPTService;

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
            _manageDwLListService = new ManageDwLListService();
            _manageUMConfigService = new ManageUMConfigService();
            _manageDwLListExtraPTService = new ManageDwLListExtraPTService();
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
        public ActionResult BettypeIndex()
        {
            return PartialView(bettypePath);
        }

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
        public JsonResult InsertBettype(HttpPostedFileBase uploadFile, bool isUpdate = false)
        {
            var result = isUpdate ? _manageBettypeService.Update(uploadFile) : _manageBettypeService.Insert(uploadFile);

            return Json(result);
        }

        [AuthenticateHost]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateBettype(UpdateBettypeRequest request) => Json(_manageBettypeService.Update(request));

        #endregion Bettype

        #region SportType
        [AuthenticateRead]
        [HttpGet]
        public ActionResult SportIndex()
        {
            return PartialView(sportPath);
        }

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
        public ActionResult ProductIndex()
        {
            return PartialView(productPath);
        }

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
        public ActionResult SBMIndex()
        {
            var viewModel = new ManageProductSettingViewModel
            {
                Products = _manageSportBettypeMatrixService.GetListProducts()
            };

            return PartialView(sportBettypeMatrixPath, viewModel);
        }

        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectSportBettypeMatrix(SelectSportBettypeMatrixRequest request) => Json(_manageSportBettypeMatrixService.Select(new SelectSportBettypeMatrixCriteria
        {
            SportType = request.SportType,
            SportName = request.SportName,
            BetType = request.BetType,
            BetTypeName = request.BetTypeName,
            ProductIds = request.ProductId,
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
        [HttpGet]
        public ActionResult CanSeeIndex()
        {
            var site = _SMAManagementService.GetSites(1).ToList();
            site.Insert(0, new IdNameEntity { Id = 0, Name = "All Sites" });
            var currency = _manageCanSeeService.GetCurrencies();
            var sports = _manageCanSeeService.GetAllSportType();
            var viewModel = new ManageProductSettingViewModel
            {
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
                    Text = $"[{s.Id.ToString()}] {s.Name}",
                    Value = s.Id.ToString()
                })
            };

            return PartialView(canSeePath, viewModel);
        }

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
        public ActionResult CSSBMIndex()
        {
            var viewModel = new ManageProductSettingViewModel
            {
                Products = _manageSportBettypeMatrixService.GetListProducts()
            };

            return PartialView(csSportBettypeMatrixPath, viewModel);
        }

        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectCSSportBettypeMatrix(SelectCSSportBettypeMatrixRequest request) => Json(_manageCSSportBettypeMatrixService.Select(new SelectCSSportBettypeMatrixCriteria
        {
            SportType = request.SportType,
            SportName = request.SportName,
            BetType = request.BetType,
            BetTypeName = request.BetTypeName,
            ProductIds = request.ProductId == null ? "" : request.ProductId,
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
        public ActionResult RefResourceIndex()
        {
            return PartialView(refResourcePath);
        }

        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectRefResource(SelectRefResourceRequest request) => Json(_manageRefResourceService.Select(request), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertRefResource(HttpPostedFileBase uploadFile, bool isUpdate = false)
        {
            var result = isUpdate ? _manageRefResourceService.UpdateMultiple(uploadFile) : _manageRefResourceService.Insert(uploadFile);

            return Json(result);
        }

        [AuthenticateHost]
        [HttpPost]
        public JsonResult UpdateRefResource(UpdateRefResourceRequest request) => Json(_manageRefResourceService.Update(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult DeleteRefResource(DeleteRefResourceRequest request) => Json(_manageRefResourceService.Delete(request));

        #endregion

        #region UMConfig
        [AuthenticateRead]
        [HttpGet]
        public ActionResult UmConfigIndex()
        {
            var currency = _manageCanSeeService.GetCurrencies();
            var viewModel = new ManageProductSettingViewModel
            {
                UMCurrencies = currency.Select(c => new SelectListItem
                {
                    Text = $"[{c.Id.ToString()}] {c.Name}",
                    Value = c.Id.ToString()
                })
            };

            return PartialView(umConfigPath, viewModel);
        }

        [AuthenticateRead]
        [HttpGet]
        public JsonResult GetAllUMConfiguration() => Json(_manageUMConfigService.GetAllUMConfiguration(), JsonRequestBehavior.AllowGet);


        [AuthenticateHost]
        [HttpPost]
        public JsonResult UpdateUMConfiguration(UMConfigurationEntity entity) => Json(_manageUMConfigService.UpdateUMConfiguration(entity));

        [AuthenticateRead]
        [HttpGet]
        public JsonResult GetProductSettingByFunction() => Json(_manageUMConfigService.GetProductSettingByFunction(), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertUMConfiguration(UMConfigurationEntity entity) => Json(_manageUMConfigService.InsertUMConfiguration(entity));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult DeleteUMConfiguration(int configId) => Json(_manageUMConfigService.DeleteUMConfiguration(configId));

        [AuthenticateHost]
        [HttpGet]
        public JsonResult CheckIsInUMTime(int configId) => Json(_manageUMConfigService.CheckIsInUMTime(configId), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult DeleteOldData() => Json(_manageUMConfigService.DeleteOldData());

        [AuthenticateRead]
        [HttpGet]
        public JsonResult GetCreditSites() {
            var sites = _SMAManagementService.GetSites().ToList();
            sites.Insert(0, new IdNameEntity { Id = 0, Name = "All Sites" });

            return Json(sites.Select(s => new SelectListItem
            {
                Text = $"[{s.Id.ToString()}] {s.Name}",
                Value = s.Id.ToString()
            }), JsonRequestBehavior.AllowGet);
        }

        [AuthenticateRead]
        [HttpGet]
        public JsonResult GetAllSites()
        {
            var sites = _manageUMConfigService.GetAllSites().ToList();
            sites.Insert(0, new IdNameEntity { Id = 0, Name = "All Sites" });

            return Json(sites.Select(s => new SelectListItem
            {
                Text = $"[{s.Id.ToString()}] {s.Name}",
                Value = s.Id.ToString()
            }), JsonRequestBehavior.AllowGet);
        }

        [AuthenticateRead]
        [HttpGet]
        public JsonResult GetMarkets()
        {
            var markets = _manageUMConfigService
                .GetMarkets()
                .Select(m => new SelectListItem
                {
                    Text = $"[{m.Id}] {m.Name}",
                    Value = m.Id.ToString()
                });

            return Json(markets, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ResetWinning

        [AuthenticateRead]
        [HttpGet]
        public ActionResult ResetWinningIndex()
        {
            return PartialView(resetWinningPath);
        }

        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectDWLList(SelectDwLListRequest request) => Json(_manageDwLListService.Select(request), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertDWLList(HttpPostedFileBase uploadFile) => Json(_manageDwLListService.Insert(uploadFile, false));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertDWLListTest(HttpPostedFileBase uploadFile) => Json(_manageDwLListService.Insert(uploadFile, true));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult UpdateDWLList(UpdateDwLListRequest request) => Json(_manageDwLListService.Update(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult DeleteDWLList(DeleteDwLListRequest request) => Json(_manageDwLListService.Delete(request));

        [AuthenticateRead]
        [HttpGet]
        public JsonResult SelectDWLListExtraPT(SelectDwLListExtraPTRequest request) => Json(_manageDwLListExtraPTService.Select(request), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertDWLListExtraPT(HttpPostedFileBase uploadFile) => Json(_manageDwLListExtraPTService.Insert(uploadFile, false));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult InsertDWLListExtraPTTest(HttpPostedFileBase uploadFile) => Json(_manageDwLListExtraPTService.Insert(uploadFile, true));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult UpdateDWLListExtraPT(UpdateDwLListExtraPTRequest request) => Json(_manageDwLListExtraPTService.Update(request));

        [AuthenticateHost]
        [HttpPost]
        public JsonResult DeleteDWLListExtraPT(DeleteDwLListExtraPTRequest request) => Json(_manageDwLListExtraPTService.Delete(request));

        #endregion
        #region Vendor Global Setting
        [AuthenticateRead]
        [HttpGet]
        public ActionResult VendorIndex()
        {
            var sports = _manageCanSeeService.GetAllSportType();
            var viewModel = new ManageProductSettingViewModel
            {
                SportTypes = sports.Select(s => new SelectListItem
                {
                    Text = $"[{s.Id.ToString()}] {s.Name}",
                    Value = s.Id.ToString()
                })
            };

            return PartialView(vendorPath, viewModel);
        }

        #endregion

        #region Product3rd
        [AuthenticateRead]
        [HttpGet]
        public ActionResult Product3rdIndex()
        {
            return PartialView(product3rdPath);
        }

        #endregion
    }
}