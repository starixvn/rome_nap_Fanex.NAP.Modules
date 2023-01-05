namespace Fanex.Tarus.Admin.ManageProductSetting.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageRefResource;
    using Fanex.Tarus.Admin.ManageProductSetting.DataAccess;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageRefResource;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Extensions;
    using Fanex.Tarus.Infrastructure.Helper;
    using NPOI.SS.UserModel;

    public interface IManageRefResourceService
    {
        RefResourceModel Select(SelectRefResourceRequest request);
        Result Insert(HttpPostedFileBase uploadFile);
        Result UpdateMultiple(HttpPostedFileBase uploadFile);
        Result Update(UpdateRefResourceRequest request);
        Result Delete(DeleteRefResourceRequest request);
    }
    public class ManageRefResourceService : IManageRefResourceService
    {
        private readonly IManageRefResourceDataAccess dataAccess;

        public ManageRefResourceService()
        {
            dataAccess = new ManageRefResourceDataAccess();
        }

        public Result Delete(DeleteRefResourceRequest request)
             => dataAccess.Delete(new DeleteRefResourceCriteria(request.RefId, request.ResourceId, AppSession.AccId, Utils.GetClientIP()));

        public Result Insert(HttpPostedFileBase uploadFile)
        {
            InsertRefResourceCriteria criteria = new InsertRefResourceCriteria();
            try
            {
                criteria.RefResources = ParseBetTypeXML(uploadFile);
                criteria.UserId = AppSession.AccId;
                criteria.LoginIP = Utils.GetClientIP();
            }
            catch (Exception ex)
            {
                return new Result(1, ex.Message);
            }

            return dataAccess.Insert(criteria);
        }

        public RefResourceModel Select(SelectRefResourceRequest request)
            => dataAccess.Select(new SelectRefResourceCriteria
            {
                RefId = request.RefId,
                ResourceId = request.ResourceId,
                ResourceEN = request.ResourceEN,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex
            });

        public Result Update(UpdateRefResourceRequest request)
        {
            var criteria = new UpdateRefResourceCriteria
            {
                IsMultiple = false,
                UserId = AppSession.AccId,
                LoginIP = Utils.GetClientIP()
            };

            criteria.RefResources.Add(new RefResourceEntity {
                RefId = request.RefId,
                ResourceId = request.Id,
                ResourceCH = request.ResourceCH,
                ResourceCS = request.ResourceCS,
                ResourceDE = request.ResourceDE,
                ResourceEL = request.ResourceEL,
                ResourceEN = request.ResourceEN,
                ResourceES = request.ResourceES,
                ResourceFR = request.ResourceFR,
                ResourceHI = request.ResourceHI,
                ResourceID = request.ResourceID,
                ResourceIT = request.ResourceIT,
                ResourceJP = request.ResourceJP,
                ResourceKO = request.ResourceKO,
                ResourceMM = request.ResourceMM,
                ResourcePL = request.ResourcePL,
                ResourcePT = request.ResourceRU,
                ResourceRU = request.ResourceRU,
                ResourceTH = request.ResourceTH,
                ResourceVN = request.ResourceVN,
                RowNum = request.RowNum,
                Sort1 = request.Sort1,
                Sort2 = request.Sort2
            });

            return dataAccess.Update(criteria);
        }
            

        public Result UpdateMultiple(HttpPostedFileBase uploadFile)
        {
            var criteria = new UpdateRefResourceCriteria
            {
                IsMultiple = true,
                UserId = AppSession.AccId,
                LoginIP = Utils.GetClientIP()
            };
            try
            {
                criteria.RefResources = ParseBetTypeXML(uploadFile);
                criteria.UserId = AppSession.AccId;
                criteria.LoginIP = Utils.GetClientIP();
            }
            catch (Exception ex)
            {
                return new Result(1, ex.Message);
            }

            return dataAccess.Update(criteria);
        }

        private List<RefResourceEntity> ParseBetTypeXML(HttpPostedFileBase uploadFile)
        {
            var result = new List<RefResourceEntity>();
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
                    var res = new RefResourceEntity
                    {
                        RefId = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString()),
                        ResourceId = HttpUtility.HtmlEncode(row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString()),
                        ResourceEN = HttpUtility.HtmlEncode(row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString()),
                        ResourceCH = HttpUtility.HtmlEncode(row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString()),
                        ResourceCS = HttpUtility.HtmlEncode(row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString()),
                        ResourceJP = HttpUtility.HtmlEncode(row.GetCell(5) == null ? string.Empty : row.GetCell(5).ToString()),
                        ResourceIT = HttpUtility.HtmlEncode(row.GetCell(6) == null ? string.Empty : row.GetCell(6).ToString()),
                        ResourceTH = HttpUtility.HtmlEncode(row.GetCell(7) == null ? string.Empty : row.GetCell(7).ToString()),
                        ResourceKO = HttpUtility.HtmlEncode(row.GetCell(8) == null ? string.Empty : row.GetCell(8).ToString()),
                        ResourceES = HttpUtility.HtmlEncode(row.GetCell(9) == null ? string.Empty : row.GetCell(9).ToString()),
                        Sort1 = HttpUtility.HtmlEncode(row.GetCell(10) == null ? string.Empty : row.GetCell(10).ToString()),
                        Sort2 = HttpUtility.HtmlEncode(row.GetCell(11) == null ? string.Empty : row.GetCell(11).ToString()),
                        ResourceEL = HttpUtility.HtmlEncode(row.GetCell(12) == null ? string.Empty : row.GetCell(12).ToString()),
                        ResourceVN = HttpUtility.HtmlEncode(row.GetCell(13) == null ? string.Empty : row.GetCell(13).ToString()),
                        ResourcePL = HttpUtility.HtmlEncode(row.GetCell(14) == null ? string.Empty : row.GetCell(14).ToString()),
                        ResourceID = HttpUtility.HtmlEncode(row.GetCell(15) == null ? string.Empty : row.GetCell(15).ToString()),
                        ResourceHI = HttpUtility.HtmlEncode(row.GetCell(16) == null ? string.Empty : row.GetCell(16).ToString()),
                        ResourceMM = HttpUtility.HtmlEncode(row.GetCell(17) == null ? string.Empty : row.GetCell(17).ToString()),
                        ResourceFR = HttpUtility.HtmlEncode(row.GetCell(18) == null ? string.Empty : row.GetCell(18).ToString()),
                        ResourceRU = HttpUtility.HtmlEncode(row.GetCell(19) == null ? string.Empty : row.GetCell(19).ToString()),
                        ResourcePT = HttpUtility.HtmlEncode(row.GetCell(20) == null ? string.Empty : row.GetCell(20).ToString()),
                        ResourceDE = HttpUtility.HtmlEncode(row.GetCell(21) == null ? string.Empty : row.GetCell(21).ToString())
                    };
                    
                    if(res.IsHaveData) result.Add(res);
                }

                return result;
            }
            catch (Exception ex)
            {
                LogExtensions.ExceptionHandler(ex);
                throw new Exception("Invalid file uploaded. Error at line: " + rowCount.ToString());
            }
        }
    }
}
