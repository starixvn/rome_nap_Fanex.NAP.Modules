using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Fanex.Tarus.Admin.Criteria.ManageProduct;
using Fanex.Tarus.Admin.DataAccess.ManageProduct;
using Fanex.Tarus.Admin.Entities;
using Fanex.Tarus.Admin.Entities.ManageProduct;
using Fanex.Tarus.Admin.Models.ManageProduct;
using Fanex.Tarus.Infrastructure.Common;
using Fanex.Tarus.Infrastructure.Helper;
using NPOI.SS.UserModel;

namespace Fanex.Tarus.Admin.Business.ManageProduct
{
    public class ManageProductService
    {
        private readonly ManageProductDataAccess _manageProductDataAccess;

        public ManageProductService() : this(null)
        {
        }

        public ManageProductService(ManageProductDataAccess manageSporttypeDataAccess)
        {
            _manageProductDataAccess = manageSporttypeDataAccess ?? new ManageProductDataAccess();
        }

        public ProductModel Select(SelectProductCriteria criteria) => _manageProductDataAccess.Select(criteria);

        public Result Insert(HttpPostedFileBase uploadFile)
        {
#pragma warning disable S1854 // Unused assignments should be removed
            var result = new Result();
#pragma warning restore S1854 // Unused assignments should be removed
            InsertProductCriteria criteria = new InsertProductCriteria();
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
                    criteria.Entities.Add(new ProductEntity
                    {
                        ProductID = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString()),
                        ProductName = HttpUtility.HtmlEncode(row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString()),
                        Description = HttpUtility.HtmlEncode(row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString()),
                        ProductPrefix = HttpUtility.HtmlEncode(row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString()),
                        Enabled = bool.Parse(row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString()),
                        EndOfSupportDate = HttpUtility.HtmlEncode(row.GetCell(5) == null ? string.Empty : row.GetCell(5).ToString()),
                        BalanceType = HttpUtility.HtmlEncode(row.GetCell(6) == null ? string.Empty : row.GetCell(6).ToString()),
                        SubProductBalanceType = HttpUtility.HtmlEncode(row.GetCell(7) == null ? string.Empty : row.GetCell(7).ToString()),
                        GroupType = HttpUtility.HtmlEncode(row.GetCell(8) == null ? string.Empty : row.GetCell(8).ToString()),
                        CasinoType = HttpUtility.HtmlEncode(row.GetCell(9) == null ? string.Empty : row.GetCell(9).ToString()),
                        IsOfferPlayForFun = bool.Parse(row.GetCell(10) == null ? string.Empty : row.GetCell(10).ToString()),
                        VendorID = HttpUtility.HtmlEncode(row.GetCell(11) == null ? string.Empty : row.GetCell(11).ToString()),
                        OrderNo = HttpUtility.HtmlEncode(row.GetCell(12) == null ? string.Empty : row.GetCell(12).ToString())
                    });
                }

                criteria.UserID = AppSession.AccId;
                criteria.LoginIP = Utils.GetClientIP();
                result = this._manageProductDataAccess.Insert(criteria);
            }
            catch (Exception ex)
            {
                result.ErrorCode = 1;
                result.ErrorMessage = "Invalid file uploaded. Error at line: " + rowCount.ToString();
                Logging.Logger.Log.Error(result.ErrorMessage, ex);
                return result;
            }

            return result;
        }

        public Result Update(ProductEntity request)
        {
            request.SubProductBalanceType = HttpUtility.HtmlEncode(HttpUtility.UrlDecode(request.SubProductBalanceType));

            return _manageProductDataAccess.Update(new UpdateProductCriteria
            {
                Entities = new List<ProductEntity> { request }
            });
        }
    }
}