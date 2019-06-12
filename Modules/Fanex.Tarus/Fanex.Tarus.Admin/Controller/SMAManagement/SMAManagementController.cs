using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fanex.Tarus.Admin.Business.SMAManagement;
using Fanex.Tarus.Admin.Criteria.SMAManagement;
using Fanex.Tarus.Admin.Entities;
using Fanex.Tarus.Admin.Entities.SMAManagement;
using Fanex.Tarus.Admin.Resource;
using Fanex.Tarus.Infrastructure.Common;
using Fanex.Tarus.Infrastructure.Helper;
using Fanex.Tarus.Security.Services;
using Fanex.Tarus.Web.Controllers;
using Fanex.Tarus.Web.Controllers.Filters;

namespace Fanex.Tarus.Admin.Controller.SMAManagement
{
    public class SMAManagementController : BaseController
    {
        readonly ISMAManagementService _smaManagementService;      

        public SMAManagementController()
            : this(null)
        {
        }

        public SMAManagementController(ISMAManagementService smaManagemenentService)
        {
            _smaManagementService = smaManagemenentService ?? new SMAManagementService();
        }

        [AuthenticateRead]
        public ActionResult Index()
        {
            ViewData["HasHostPermission"] = HasHostPermission(Sunplus.Accounting.Core.Common.Enumerations.Admin.SMAManagement);

            return View("~/Views/Admin/SMAManagement/SMAManagement.cshtml", GetFilterInfo());
        }

        public ActionResult CN88()
        {
            ViewData["HasHostPermission"] = HasHostPermission(Sunplus.Accounting.Core.Common.Enumerations.Admin.CN88SMAManagement);

            return View("~/Views/Admin/SMAManagement/CN88SMAManagement.cshtml", GetCN88FilterInfo());
        }

        [HttpGet]
        public ActionResult GetCurrencies(bool excludeTestCurrencies)
        {
            var currencies = _smaManagementService.GetCurrencies(excludeTestCurrencies).ToList();
            currencies.Insert(0, new IdNameEntity { Name = Language.All, Id = 0 });

            return Json(currencies, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSMAGroupHeadlines(GetSMAGroupHeadlineCriteria criteria)
        {
            var result = _smaManagementService.GetSMAGroupHeadlines(criteria);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost] 
        public JsonResult GetCN88SMAGroupHeadlines(GetCN88SMAGroupHeadlineCriteria criteria)
        {
            var result = _smaManagementService.GetCN88SMAGroupHeadlines(criteria);

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult ExportSMAGroupHeadlinesToExcel(GetSMAGroupHeadlineCriteria criteria)
        {
            string reportName = "SMAListByGroupHeadline";
            var excelFile = _smaManagementService.ExportSMAGroupHeadlinesToExcel(reportName, criteria);

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", reportName));

            excelFile.SaveXls(Response.OutputStream);

            return new EmptyResult();
        }

        public ActionResult ExportCN88SMAGroupHeadlinesToExcel(GetCN88SMAGroupHeadlineCriteria criteria)
        {
            string reportName = "CN88SMAListByGroupHeadline";
            var excelFile = _smaManagementService.ExportCN88SMAGroupHeadlinesToExcel(reportName, criteria);

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", reportName));

            excelFile.SaveXls(Response.OutputStream);

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult UpdateHeadlineType(UpdateHeadlineTypeCriteria criteria)
        {
            Result result;

            if (!HasHostPermission(Sunplus.Accounting.Core.Common.Enumerations.Admin.SMAManagement))
            {
                result = new Result(403, "You are not authorized on this function!");
            }
            else
            {
                criteria.UserId = AppSession.AccId;
                criteria.IP = Utils.GetClientIP();

                result = _smaManagementService.UpdateHeadlineType(criteria);
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult UpdateCN88HeadlineType(UpdateHeadlineTypeCriteria criteria)
        {
            Result result;

            if (!HasHostPermission(Sunplus.Accounting.Core.Common.Enumerations.Admin.CN88SMAManagement))
            {
                result = new Result(403, "You are not authorized on this function!");
            }
            else
            {
                criteria.UserId = AppSession.AccId;
                criteria.IP = Utils.GetClientIP();

                result = _smaManagementService.UpdateHeadlineType(criteria);
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult UpdateOwner(UpdateOwnerCriteria criteria)
        {
            Result result;

            if (!HasHostPermission(Sunplus.Accounting.Core.Common.Enumerations.Admin.SMAManagement))
            {
                result = new Result(403, "You are not authorized on this function!");
            }
            else
            {
                criteria.UserId = AppSession.AccId;
                criteria.IP = Utils.GetClientIP();

                result = _smaManagementService.UpdateOwner(criteria);
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult UpdateCN88Owner(UpdateOwnerCriteria criteria)
        {
            Result result;

            if (!HasHostPermission(Sunplus.Accounting.Core.Common.Enumerations.Admin.CN88SMAManagement))
            {
                result = new Result(403, "You are not authorized on this function!");
            }
            else
            {
                criteria.UserId = AppSession.AccId;
                criteria.IP = Utils.GetClientIP();

                result = _smaManagementService.UpdateOwner(criteria);
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [NonAction]
        FilterGroup GetFilterInfo()
        {
            var sites = _smaManagementService.GetSites().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });

            var currencies = _smaManagementService.GetCurrencies(false).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            currencies.Insert(0, new SelectListItem { Text = Language.All, Value = "0", Selected = true });

            return new FilterGroup
            {
                Sites = sites,
                Currencies = currencies,
                Statuses = GetStatuses(),
                HeadlineTypes = GetHeadlineTypes(),
                Positions = GetPositions()
            };
        }

        [NonAction]
        FilterGroup GetCN88FilterInfo()
        {
            var currencies = _smaManagementService.GetCurrencies(false).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            currencies.Insert(0, new SelectListItem { Text = Language.All, Value = "0", Selected = true });

            return new FilterGroup
            {
                Sites = null,
                Currencies = currencies,
                Statuses = GetCN88Statuses(),
                HeadlineTypes = GetHeadlineTypes(),
                Positions = GetPositions()
            };
        }


        [NonAction]
        IEnumerable<SelectListItem> GetStatuses()
        {

            return new List<SelectListItem>
            {
                new SelectListItem { Text = Language.Open, Value = "isOpen" },
                new SelectListItem { Text = Language.Suspended, Value = "Suspended" },
                new SelectListItem { Text = Language.Closed, Value = "isClosed" },
                new SelectListItem { Text = Language.Disabled, Value = "isDisabled" }
            };
        }

        [NonAction]
        IEnumerable<SelectListItem> GetCN88Statuses()
        {

            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Active", Value = "isActive" },
                new SelectListItem { Text = "View Only", Value = "isViewOnly" },
                new SelectListItem { Text = "Suspended" , Value = "isSuspended" },
                new SelectListItem { Text = "Inactive", Value = "isInactive" },
                new SelectListItem { Text = Language.Disabled, Value = "isDisabled" }
            };
        }

        [NonAction]
        IEnumerable<SelectListItem> GetHeadlineTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = Language.All, Value = string.Empty, Selected = true },
                new SelectListItem { Text = Language.VIP, Value = Language.VIP },
                new SelectListItem { Text = Language.Normal, Value = Language.Normal }
            };
        }

        [NonAction]
        IEnumerable<SelectListItem> GetPositions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = Language.All, Value = "0", Selected = true },
                new SelectListItem { Text = Language.Super, Value = "4" },
                new SelectListItem { Text = Language.Master, Value = "3" },
                new SelectListItem { Text = Language.Agent, Value = "2" },
                new SelectListItem { Text = Language.Member, Value = "1" }
            };
        }

        [NonAction]
        bool HasHostPermission(Sunplus.Accounting.Core.Common.Enumerations.Admin permission)
        {
            var permissionService = new PermissionService();
            var roleAccess = permissionService.GetRoleAccess(AppSession.AccId);

            return permissionService.GetHostPermission(permission, roleAccess);
        }
    }
}
