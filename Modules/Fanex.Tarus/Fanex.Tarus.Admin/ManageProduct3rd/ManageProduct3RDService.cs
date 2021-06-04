using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Fanex.Tarus.Admin.Entities;
using Fanex.Tarus.Admin.ManageProduct3RD.Criteria;
using Fanex.Tarus.Admin.ManageProduct3RD.Models;
using Fanex.Tarus.Infrastructure.Extensions;
using NPOI.SS.UserModel;

namespace Fanex.Tarus.Admin.ManageProduct3RD
{
    public class ManageProduct3RDService
    {
        private readonly IManageProduct3RDDataAccess dataAccess;

        public ManageProduct3RDService() : this(null)
        {
        }

        public ManageProduct3RDService(IManageProduct3RDDataAccess manageProduct3RDDataAccess)
        {
            dataAccess = manageProduct3RDDataAccess ?? new ManageProduct3RDDataAccess();
        }

        public Product3RDModel Select(SelectProduct3RDCriteria criteria) => dataAccess.Select(criteria);

        public Result Insert(HttpPostedFileBase uploadFile)
        {
#pragma warning disable S1854 // Unused assignments should be removed
            var result = new Result();
#pragma warning restore S1854 // Unused assignments should be removed
            InsertProduct3RDCriteria criteria = new InsertProduct3RDCriteria();
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

                    criteria.Entities.Add(new Product3RDEntity
                    {
                        ProductId = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString()),
                        ProductName = HttpUtility.HtmlEncode(row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString()),
                        Description = HttpUtility.HtmlEncode(row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString()),
                        ProductPrefix = HttpUtility.HtmlEncode(row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString()),
                        OfferSingle = HttpUtility.HtmlEncode(row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString()),
                        ApplyDCS = HttpUtility.HtmlEncode(row.GetCell(5) == null ? string.Empty : row.GetCell(5).ToString()),
                        Enabled = HttpUtility.HtmlEncode(row.GetCell(6) == null ? string.Empty : row.GetCell(6).ToString()),
                        EndOfSupportDate = HttpUtility.HtmlEncode(row.GetCell(7) == null ? string.Empty : row.GetCell(7).ToString()),
                        OfferSite = HttpUtility.HtmlEncode(row.GetCell(8) == null ? string.Empty : row.GetCell(8).ToString())
                    });
                }

                result = this.dataAccess.Insert(criteria);
            }
            catch (Exception ex)
            {
                result.ErrorCode = 1;
                result.ErrorMessage = "Invalid file uploaded. Error at line: " + rowCount.ToString();
                LogExtensions.ExceptionHandler(ex);
                return result;
            }

            return result;
        }

        public Result Update(Product3RDEntity request) => dataAccess.Update(new UpdateProduct3RDCriteria
        {
            Entities = new List<Product3RDEntity> { request }
        });

        public Result Delete(string productId) => dataAccess.Delete(new DeleteProduct3RDCriteria
        {
            ProductId = productId
        });
    }
    
}
