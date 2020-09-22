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
    using Fanex.Tarus.Infrastructure.Helper;
    using NPOI.SS.UserModel;

    public interface IManageDwLListExtraPTService
    {
        IEnumerable<DwLListExtraPTEntity> Select(SelectDwLListExtraPTRequest request);

        Result Insert(HttpPostedFileBase uploadFile, bool isTest);

        Result Update(UpdateDwLListExtraPTRequest request);

        Result Delete(DeleteDwLListExtraPTRequest request);
    }
    public class ManageDwLListExtraPTService : IManageDwLListExtraPTService
    {
        private readonly IManageDwLListExtraPTDataAccess dataAccess;

        public ManageDwLListExtraPTService()
        {
            dataAccess = new ManageDwLListExtraPTDataAccess();
        }

        public Result Delete(DeleteDwLListExtraPTRequest request)
            => dataAccess.Delete(new DeleteDwLListExtraPTCriteria
            {
                CustId = request.CustId,
                IsTest = request.IsTest,
                UserId = AppSession.AccId,
                LoginIP = Utils.GetClientIP()
            });

        public Result Insert(HttpPostedFileBase uploadFile, bool isTest)
        {
            InsertDwLListExtraPTCriteria criteria = new InsertDwLListExtraPTCriteria();
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
                    criteria.DwLListExtraPT.Add(new DwLListExtraPTEntity
                    {
                        CustId = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString()),
                        GroupName = HttpUtility.HtmlEncode(row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString()),
                        GroupId = HttpUtility.HtmlEncode(row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString()),
                        IsDisabled = HttpUtility.HtmlEncode(row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString()),
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
                Logging.Logger.Log.Error(result.ErrorMessage, ex);

                return result;
            }
        }

        public IEnumerable<DwLListExtraPTEntity> Select(SelectDwLListExtraPTRequest request)
            => dataAccess.Select(new SelectDwLListExtraPTCriteria
            {
                CustID = request.CustID,
                GroupID = request.GroupID,
                GroupName = request.GroupName,
                Username = request.Username,
                IsDisabled = request.IsDisabled,
                IsTest = request.IsTest,

            });

        public Result Update(UpdateDwLListExtraPTRequest request)
        {
            var entity = new DwLListExtraPTEntity
            {
                CustId = request.CustId,
                GroupId = request.GroupId,
                GroupName = request.GroupName,
                Username = request.Username,
                IsDisabled = request.IsDisabled
            };

            var criteria = new UpdateDwLListExtraPTCriteria
            {
                DwLListExtraPT = new List<DwLListExtraPTEntity> { entity },
                IsTest = request.IsTest,
                UserId = AppSession.AccId,
                LoginIP = Utils.GetClientIP()
            };

            return dataAccess.Update(criteria);
        }
    }
}
