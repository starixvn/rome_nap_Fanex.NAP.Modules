namespace Fanex.Tarus.Admin.ManageProductSetting.Services
{
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeSettingMatrix;
    using Fanex.Tarus.Admin.ManageProductSetting.DataAccess;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageSportBettypeSettingMatrix;
    using Fanex.Tarus.Infrastructure.Extensions;
    using NPOI.SS.UserModel;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    public interface IManageSbsmService
    {
        SbsmModel Select(SelectSbsmRequest request);
        Result Insert(HttpPostedFileBase uploadFile);
        Result UpdateMultiple(HttpPostedFileBase uploadFile);
        Result Update(SbsmEntity request);
        Result Delete(IEnumerable<SbsmEntity> entities);
        SbsmFunctionTableModel GetFunctionTables();
        IEnumerable<SelectListItem> GetProducts();
        IEnumerable<SelectListItem> GetSportTypes(GetSportTypeRequest request);
        IEnumerable<SelectListItem> GetBetTypes(GetBetTypeRequest request);
        IEnumerable<SelectListItem> GetDummyProducts();
    }

    public class ManageSbsmService : IManageSbsmService
    {
        private readonly IManageSbsmDataAccess dataAccess;
        public ManageSbsmService()
        {
            dataAccess = new ManageSbsmDataAccess();
        }

        public Result Delete(IEnumerable<SbsmEntity> entities)
        {
            var criteria = new DeleteSbsmCriteria(entities);

            return dataAccess.Delete(criteria);
        }

        public IEnumerable<SelectListItem> GetBetTypes(GetBetTypeRequest request)
        {
            return dataAccess.GetBetType(new RefSettingBettypeSelCriteria() { ProductIds = request.ProductIds, SportTypes = request.SportTypes, BetTypes = "0" });
        }

        public SbsmFunctionTableModel GetFunctionTables()
        {
            return dataAccess.GetAllFunctionTable();
        }

        public IEnumerable<SelectListItem> GetProducts()
        {
            return dataAccess.GetProduct(new RefSettingProductSelCriteria() { ProductIds = "0" });
        }

        public IEnumerable<SelectListItem> GetDummyProducts()
        {
            return dataAccess.GetProduct(new RefSettingDummyProductSelCriteria() { ProductIds = "0" });
        }

        public IEnumerable<SelectListItem> GetSportTypes(GetSportTypeRequest request)
        {
            return dataAccess.GetSportType(new RefSettingSportTypeSelCriteria() { ProductIds = request.ProductIds, SportTypes = "0" });
        }

        public Result Insert(HttpPostedFileBase uploadFile)
        {
            var parseResult = ParseXMLEntities(uploadFile, out List<SbsmEntity> entities);
            if (parseResult.ErrorCode != 0)
            {
                return parseResult;
            }

            var checkResult = dataAccess.Check(new CheckSbsmCriteria(entities, 1, 0));
            if (checkResult.ErrorCode != 0)
            {
                return checkResult;
            }

            var criteria = new InsertSbsmCriteria(entities);
            return dataAccess.Insert(criteria);
        }

        public SbsmModel Select(SelectSbsmRequest request)
        {
            var criteria = new SelectSbsmCriteria()
            {
                ProductIds = request.ProductId,
                SportTypes = request.SportTypes,
                BetTypes = request.BetTypes,
                IsLive = request.IsLive,
                IsLicensee = request.IsLicensee,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                ExportData = 0
            };

            var result = dataAccess.Select(criteria);
            result.DummyProducts = GetDummyProducts();

            return result;
        }

        public Result Update(SbsmEntity request)
        {
            var entities = new List<SbsmEntity>() { request };
            var checkResult = dataAccess.Check(new CheckSbsmCriteria(entities, 0, 1));
            if (checkResult.ErrorCode != 0)
            {
                return checkResult;
            }

            var criteria = new UpdateSbsmCriteria(entities, 1);
            return dataAccess.Insert(criteria);
        }

        public Result UpdateMultiple(HttpPostedFileBase uploadFile)
        {
            var parseResult = ParseXMLEntities(uploadFile, out List<SbsmEntity> entities);
            if (parseResult.ErrorCode != 0)
            {
                return parseResult;
            }

            var checkResult = dataAccess.Check(new CheckSbsmCriteria(entities, 0, 0));
            if (checkResult.ErrorCode != 0)
            {
                return checkResult;
            }

            var criteria = new UpdateSbsmCriteria(entities, 0);
            return dataAccess.Insert(criteria);
        }

        private Result ParseXMLEntities(HttpPostedFileBase uploadFile, out List<SbsmEntity> entities)
        {
            entities = new List<SbsmEntity>();
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
                    entities.Add(new SbsmEntity
                    {
                        SportType = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString()),
                        BetType = HttpUtility.HtmlEncode(row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString()),
                        IsLicensee = HttpUtility.HtmlEncode(row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString()),
                        IsLive = HttpUtility.HtmlEncode(row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString()),
                        Site = HttpUtility.HtmlEncode(row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString()),
                        SportTypeRefPT = HttpUtility.HtmlEncode(row.GetCell(5) == null ? string.Empty : row.GetCell(5).ToString()),
                        BetTypeRefPT = HttpUtility.HtmlEncode(row.GetCell(6) == null ? string.Empty : row.GetCell(6).ToString()),
                        IsLivePT = HttpUtility.HtmlEncode(row.GetCell(7) == null ? string.Empty : row.GetCell(7).ToString()),
                        TablePT = HttpUtility.HtmlEncode(row.GetCell(8) == null ? string.Empty : row.GetCell(8).ToString()),
                        SportTypeRefComm = HttpUtility.HtmlEncode(row.GetCell(9) == null ? string.Empty : row.GetCell(9).ToString()),
                        BetTypeRefComm = HttpUtility.HtmlEncode(row.GetCell(10) == null ? string.Empty : row.GetCell(10).ToString()),
                        TableComm = HttpUtility.HtmlEncode(row.GetCell(11) == null ? string.Empty : row.GetCell(11).ToString()),
                        SportTypeRefBetSetting = HttpUtility.HtmlEncode(row.GetCell(12) == null ? string.Empty : row.GetCell(12).ToString()),
                        BetTypeRefBetSetting = HttpUtility.HtmlEncode(row.GetCell(13) == null ? string.Empty : row.GetCell(13).ToString()),
                        TableBetSetting = HttpUtility.HtmlEncode(row.GetCell(14) == null ? string.Empty : row.GetCell(14).ToString()),
                        TableBettrans = HttpUtility.HtmlEncode(row.GetCell(15) == null ? string.Empty : row.GetCell(15).ToString()),
                        SporttypeRefWinlossLimit = HttpUtility.HtmlEncode(row.GetCell(16) == null ? string.Empty : row.GetCell(16).ToString()),
                        BettypeRefWinlossLimit = HttpUtility.HtmlEncode(row.GetCell(17) == null ? string.Empty : row.GetCell(17).ToString()),
                        RealProductId = HttpUtility.HtmlEncode(row.GetCell(18) == null ? string.Empty : row.GetCell(18).ToString()),
                        DummyProductId = HttpUtility.HtmlEncode(row.GetCell(19) == null ? string.Empty : row.GetCell(19).ToString())
                    });
                }

                return new Result();
            }
            catch (Exception ex)
            {
                LogExtensions.ExceptionHandler(ex);

                return new Result
                {
                    ErrorCode = 1,
                    ErrorMessage = "Invalid file uploaded. Error at line: " + rowCount.ToString()
                };
            }
        }
    }
}
