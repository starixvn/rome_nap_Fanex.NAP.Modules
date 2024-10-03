using Fanex.Tarus.Business.Implement.Admin;
using Fanex.Tarus.Business.Interface.Admin;
using Fanex.Tarus.Entities.Criteria;
using Fanex.Tarus.Entities.Criteria.Admin;
using Fanex.Tarus.Infrastructure.Extensions;
using Fanex.Tarus.Web.Controllers.Filters;
using Sunplus.Accounting.Core.Common;
using Sunplus.Accounting.DataAccess.IBCDLL.Accounting;
using System;
using System.Web;
using System.Web.Mvc;

namespace Fanex.Tarus.Web.Controllers.Admin
{
    public class TicketToHeadlineController : BaseController
    {
        private ITicketToHeadlineService _ticketToHeadlineService;

        public TicketToHeadlineController()
        {
            this._ticketToHeadlineService = new TicketToHeadlineService();
        }

        [AuthenticateRead]
        public ActionResult Index()
        {
            ViewBag.SupportedProducts = this._ticketToHeadlineService.GetSuportedProduct(new GetTicketToHeadlineSupportedProductCriteria());
            return View("~/Views/Admin/TicketToHeadline/Index.cshtml");
        }

        public ActionResult GetData(HttpPostedFileBase uploadFile, TicketToHeadlineCriteria criteria)
        {
            criteria.DeniedList = AccountCredit.Acc_DeniedSiteCurrency_Sel(AppSession.AccId);
            return Json(_ticketToHeadlineService.GetData(uploadFile, criteria), "text/html");
        }

        public ActionResult GetDataForPaging(TicketToHeadlineCriteria criteria)
        {
            return Json(_ticketToHeadlineService.GetDataFromCachedCriteria(criteria.PageSize, criteria.PageIndex));
        }

        public ActionResult NotifyHeadline()
        {
            return Json(_ticketToHeadlineService.NotifyHeadline(), "text/html");
        }

        public ActionResult ExportExcel()
        {
            var data = _ticketToHeadlineService.GetDataFromCachedCriteria(null, null);
            var excelService = new TicketToHeadlineExcelService(data);
            ExcelExtension.ResponseStreamExcel(excelService.Export(), GetExcelFileName());
            return new EmptyResult();
        }

        private string GetExcelFileName()
        {
            return string.Format("ExportedTicketToGroupHeadline{0}", DateTime.Now.ToString("yyyyMMdd_HH-mm-ss"));
        }
    }
}
