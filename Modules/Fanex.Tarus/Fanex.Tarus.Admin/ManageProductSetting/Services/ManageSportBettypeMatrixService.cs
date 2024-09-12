namespace Fanex.Tarus.Admin.ManageProductSetting.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageProduct;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeMatrix;
    using Fanex.Tarus.Admin.ManageProductSetting.DataAccess;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageSportBettypeMatrix;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Extensions;
    using Fanex.Tarus.Infrastructure.Helper;
    using NPOI.SS.UserModel;

    public class ManageSportBettypeMatrixService
    {
        private readonly IManageSportBettypeMatrixDataAccess _manageSportBettypeMatrixDataAccess;

        private readonly IManageProductDataAccess _manageProductDataAccess;

        public ManageSportBettypeMatrixService() : this(null, null)
        {
        }

        public ManageSportBettypeMatrixService(IManageSportBettypeMatrixDataAccess manageSportBettypeMatrixDataAccess, IManageProductDataAccess manageProductDataAccess)
        {
            _manageSportBettypeMatrixDataAccess = manageSportBettypeMatrixDataAccess ?? new ManageSportBettypeMatrixDataAccess();
            _manageProductDataAccess = manageProductDataAccess ?? new ManageProductDataAccess();
        }

        public IEnumerable<SelectListItem> GetListProducts()
        {
            var listProducts = _manageProductDataAccess.Select(new SelectProductCriteria() { PageSize = 256, PageIndex = 1 });
            return listProducts.ProductEntities.Select(product => new SelectListItem()
            {
                Text = $"[{product.ProductID}] {product.ProductName}",
                Value = product.ProductID
            });
        }

        public SportBettypeMatrixModel Select(SelectSportBettypeMatrixCriteria criteria) => _manageSportBettypeMatrixDataAccess.Select(criteria);

        public Result Insert(HttpPostedFileBase uploadFile)
        {
            InsertSportBettypeMatrixCriteria criteria = new InsertSportBettypeMatrixCriteria();
            try
            {
                criteria.Entities = ParseXMLFile(uploadFile);

                return _manageSportBettypeMatrixDataAccess.Insert(criteria);
            }
            catch (Exception ex)
            {
                return new Result(1, ex.Message);
            }
        }

        public Result UpdateMul(HttpPostedFileBase uploadFile)
        {
            try
            {
                UpdateSportBettypeMatrixCriteria criteria = new UpdateSportBettypeMatrixCriteria()
                {
                    Entities = ParseXMLFile(uploadFile),
                    IsMultiple = true
                };

                return _manageSportBettypeMatrixDataAccess.Update(criteria);
            }
            catch (Exception ex)
            {
                return new Result(1, ex.Message);
            }
        }

        private List<SportBettypeMatrixEntity> ParseXMLFile(HttpPostedFileBase uploadFile)
        {
            var result = new List<SportBettypeMatrixEntity>();
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
                    result.Add(new SportBettypeMatrixEntity
                    {
                        SportType = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString()),
                        BetType = HttpUtility.HtmlEncode(row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString()),
                        IsLive = HttpUtility.HtmlEncode(row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString()),
                        ProductId = HttpUtility.HtmlEncode(row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString()),
                        Enabled = HttpUtility.HtmlEncode(row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString()),
                        IsTest = HttpUtility.HtmlEncode(row.GetCell(5) == null ? string.Empty : row.GetCell(5).ToString()),
                        EventType = HttpUtility.HtmlEncode(row.GetCell(6) == null ? string.Empty : row.GetCell(6).ToString()),
                        SubProduct = HttpUtility.HtmlEncode(row.GetCell(7) == null ? string.Empty : row.GetCell(7).ToString()),
                        RefTable = HttpUtility.HtmlEncode(row.GetCell(8) == null ? string.Empty : row.GetCell(8).ToString())
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                LogExtensions.ExceptionHandler(ex);
#pragma warning disable S112 // General exceptions should never be thrown
                throw new Exception("Invalid file uploaded. Error at line: " + rowCount.ToString());
#pragma warning restore S112 // General exceptions should never be thrown
            }
        }

        public Result Update(SportBettypeMatrixEntity request) => _manageSportBettypeMatrixDataAccess.Update(new UpdateSportBettypeMatrixCriteria
        {
            Entities = new List<SportBettypeMatrixEntity> { request },
            IsMultiple = false
        });

        public Result Delete(IEnumerable<SportBettypeMatrixEntity> request) => _manageSportBettypeMatrixDataAccess.Delete(new DeleteSportBettypeMatrixCriteria(request) {
            UserId = AppSession.AccId,
            LoginIp = Utils.GetClientIP()
        });
    }
}