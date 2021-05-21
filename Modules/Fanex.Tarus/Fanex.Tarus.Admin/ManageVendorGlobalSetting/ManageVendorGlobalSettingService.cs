namespace Fanex.Tarus.Admin.ManageVendorGlobalSetting
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageVendorGlobalSetting.Criteria;
    using Fanex.Tarus.Admin.ManageVendorGlobalSetting.Models;
    using Fanex.Tarus.Infrastructure.Extensions;
    using NPOI.SS.UserModel;

    public class ManageVendorGlobalSettingService
    {
        private readonly IManageVendorGlobalSettingDataAccess dataAccess;

        public ManageVendorGlobalSettingService() : this(null)
        {
        }

        public ManageVendorGlobalSettingService(IManageVendorGlobalSettingDataAccess manageVendorGlobalSettingDataAccess)
        {
            dataAccess = manageVendorGlobalSettingDataAccess ?? new ManageVendorGlobalSettingDataAccess();
        }

        public VendorGlobalModel Select(SelectVendorGlobalCriteria criteria) => dataAccess.Select(criteria);

        public Result Insert(HttpPostedFileBase uploadFile)
        {
#pragma warning disable S1854 // Unused assignments should be removed
            var result = new Result();
#pragma warning restore S1854 // Unused assignments should be removed
            InsertVendorGlobalCriteria criteria = new InsertVendorGlobalCriteria();
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

                    criteria.Entities.Add(new VenderGlobalEntity
                    {
                        VendorId = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString()),
                        VendorName = HttpUtility.HtmlEncode(row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString()),
                        NumberValue = HttpUtility.HtmlEncode(row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString()),
                        SportType = HttpUtility.HtmlEncode(row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString()),
                        BetType = HttpUtility.HtmlEncode(row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString())
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

        public Result Update(VenderGlobalEntity request) => dataAccess.Update(new UpdateVendorGlobalCriteria
        {
            Entities = new List<VenderGlobalEntity> { request }
        });

        public Result Delete(VenderGlobalEntity request) => dataAccess.Delete(new DeleteVendorGlobalCriteria
        {
            VendorId = request.VendorId,
            VName = request.VendorName
        });
    }
}
