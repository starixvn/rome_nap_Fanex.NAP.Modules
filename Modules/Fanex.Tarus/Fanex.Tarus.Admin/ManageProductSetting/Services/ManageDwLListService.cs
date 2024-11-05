namespace Fanex.Tarus.Admin.ManageProductSetting.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageResetWinning;
    using Fanex.Tarus.Admin.ManageProductSetting.DataAccess;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageResetWinning;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Extensions;
    using Fanex.Tarus.Infrastructure.Helper;
    using NPOI.SS.UserModel;

    public interface IManageDwLListService
    {
        IEnumerable<DwLListEntity> Select(SelectDwLListRequest request);

        Result Insert(HttpPostedFileBase uploadFile, bool isTest);

        Result Update(UpdateDwLListRequest request);

        Result Delete(DeleteDwLListRequest request);
    }
    public class ManageDwLListService : IManageDwLListService
    {
        private readonly IManageDwLListDataAccess dataAccess;

        public ManageDwLListService()
        {
            dataAccess = new ManageDwLListDataAccess();
        }

        public Result Delete(DeleteDwLListRequest request)
             => dataAccess.Delete(new DeleteDwLListCriteria
             {
                 CustId = request.CustId,
                 Product = request.Product,
                 IsTest = request.IsTest,
                 UserId = AppSession.AccId,
                 LoginIP = Utils.GetClientIP()
             });

        public Result Insert(HttpPostedFileBase uploadFile, bool isTest)
        {
            InsertDwLListCriteria criteria = new InsertDwLListCriteria();
            int rowCount = 1;
            try
            {
                IWorkbook workbook = WorkbookFactory.Create(uploadFile.InputStream);
                ISheet sheet = workbook.GetSheetAt(0);
                IEnumerator rows = sheet.GetRowEnumerator();
                rows.MoveNext();
                while (rows.MoveNext())
                {
                    rowCount++;
                    IRow row = (IRow)rows.Current;
                    criteria.DwLList.Add(new DwLListEntity
                    {
                        CustId = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString()),
                        Product = HttpUtility.HtmlEncode(row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString()),
                        Seq = HttpUtility.HtmlEncode(row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString()),
                        ProductId = HttpUtility.HtmlEncode(row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString()),
                        IsDisabled = HttpUtility.HtmlEncode(row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString()),
                    });
                }
                criteria.IsTest = isTest;
                criteria.UserId = AppSession.AccId;
                criteria.LoginIP = Utils.GetClientIP();

                return dataAccess.Insert(criteria);
            }
            catch (Exception ex)
            {
                var result = new Result(1, "Invalid file uploaded. Error at line: " + rowCount.ToString() + ". " + ex.Message);
                LogExtensions.ExceptionHandler(ex);

                return result;
            }
        }

        public IEnumerable<DwLListEntity> Select(SelectDwLListRequest request)
            => dataAccess.Select(new SelectDwLListCriteria
            {
                CustID = request.CustID,
                Product = request.Product,
                ProductID = request.ProductID,
                Username = request.Username,
                IsDisabled = request.IsDisabled,
                IsTest = request.IsTest,
                
            });

        public Result Update(UpdateDwLListRequest request)
        {
            var entity = new DwLListEntity
            {
                CustId = request.CustId,
                Product = request.Product,
                ProductId = request.ProductId,
                Username = request.Username,
                Seq = request.Seq,
                IsDisabled = request.IsDisabled
            };

            var criteria = new UpdateDwLListCriteria
            {
                DwLList = new List<DwLListEntity> { entity },
                IsTest = request.IsTest,
                UserId = AppSession.AccId,
                LoginIP = Utils.GetClientIP()
            };

            return dataAccess.Update(criteria);
        }
    }
}
