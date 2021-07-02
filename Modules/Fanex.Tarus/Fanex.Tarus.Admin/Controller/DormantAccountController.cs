namespace Fanex.Tarus.Admin.Controller
{
    using System;
    using System.Web.Mvc;
    using Fanex.Tarus.Admin.DormantAccount.Request;
    using Fanex.Tarus.Admin.DormantAccount.Services;
    using Fanex.Tarus.Admin.DormantAccount.ViewModel;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;

    public class DormantAccountController : BaseController
    {
        private readonly string indexPath = "~/Views/Admin/DormantAccount/Index.cshtml";
        private readonly IDormantAccountService _service;
        private readonly IDormantAccountExcelService _serviceExcel;

        public DormantAccountController()
        {
            _service = new DormantAccountService();
            _serviceExcel = new DormantAccountExcelService();
        }

        [AuthenticateRead]
        public ActionResult Index()
            => View(indexPath, new DormantAccountFilter());

        [AuthenticateRead]
        [HttpPost]
        public ActionResult GetByDateType(SelectDormantAccountRequest request)
            => Json(_service.GetByDateType(request));

        [AuthenticateRead]
        [HttpPost]
        public ActionResult GetByUsername(SelectDormantAccountRequest request)
        => Json(_service.GetByUsername(request));

        [AuthenticateRead]
        [HttpPost]
        public ActionResult DrillDownByDateType(SelectDormantAccountRequest request)
        => Json(_service.DrillDownByDateType(request));

        [HttpGet]
        public ActionResult ExportExcelByDateType(SelectDormantAccountRequest criteria)
        {
            var fileName = $"DormantAccountList-{DateTime.Now.ToString("yyyyMMddhhmm")}";
            var excelFile = _serviceExcel.ExportExcelByDateType(criteria);

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", fileName));

            excelFile.SaveXls(Response.OutputStream);

            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult ExportExcelByUsername(SelectDormantAccountRequest criteria)
        {
            var fileName = $"DormantAccountList-{DateTime.Now.ToString("yyyyMMddhhmm")}";
            var excelFile = _serviceExcel.ExportExcelByUsername(criteria);

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", fileName));

            excelFile.SaveXls(Response.OutputStream);

            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult ExportExcelByDrilldown(SelectDormantAccountRequest criteria)
        {
            var fileName = $"DormantAccountList-{DateTime.Now.ToString("yyyyMMddhhmm")}";
            var excelFile = _serviceExcel.ExportExcelDrilldown(criteria);

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", fileName));

            excelFile.SaveXls(Response.OutputStream);

            return new EmptyResult();
        }
    }
}