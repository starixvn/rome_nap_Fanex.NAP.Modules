namespace Fanex.Tarus.Admin.ManageProductSetting.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageCSSportBettypeMatrix;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageProduct;
    using Fanex.Tarus.Admin.ManageProductSetting.DataAccess;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using Fanex.Tarus.Admin.ManageProductSetting.Request.ManageCSSportBettypeMatrix;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;
    using NPOI.SS.UserModel;

    public interface IManageCSSportBettypeMatrixService
    {
        CSSportBettypeMatrixModel Select(SelectCSSportBettypeMatrixCriteria criteria);

        Result Insert(HttpPostedFileBase uploadFile);

        Result Update(CSSportBettypeMatrixEntity request);

        Result Delete(DeleteCSSportBettypeMatrixRequest request);

        IEnumerable<BetTypeGroupEntity> GetBetTypeGroups();

        IEnumerable<SportTypeGroupEntity> GetSportTypeGroups();
    }
    public class ManageCSSportBettypeMatrixService : IManageCSSportBettypeMatrixService
    {
        private readonly IManageCSSportBettypeMatrixDataAccess _manageCSSportBettypeMatrixDataAccess;

        private readonly IManageProductDataAccess _manageProductDataAccess;

        public ManageCSSportBettypeMatrixService() : this(null, null)
        {
        }

        public ManageCSSportBettypeMatrixService(IManageCSSportBettypeMatrixDataAccess manageCSSportBettypeMatrixDataAccess, IManageProductDataAccess manageProductDataAccess)
        {
            _manageCSSportBettypeMatrixDataAccess = manageCSSportBettypeMatrixDataAccess ?? new ManageCSSportBettypeMatrixDataAccess();
            _manageProductDataAccess = manageProductDataAccess ?? new ManageProductDataAccess();
        }

        public IEnumerable<BetTypeGroupEntity> GetBetTypeGroups()
            => _manageProductDataAccess.SelectBetTypeGroup(new SelectBetTypeGroupCriteria());

        public IEnumerable<SportTypeGroupEntity> GetSportTypeGroups()
            => _manageProductDataAccess.SelectSportTypeGroup(new SelectSportTypeGroupCriteria());

        public CSSportBettypeMatrixModel Select(SelectCSSportBettypeMatrixCriteria criteria) => _manageCSSportBettypeMatrixDataAccess.Select(criteria);

        public Result Insert(HttpPostedFileBase uploadFile)
        {
#pragma warning disable S1854 // Unused assignments should be removed
            var result = new Result();
#pragma warning restore S1854 // Unused assignments should be removed
            InsertCSSportBettypeMatrixCriteria criteria = new InsertCSSportBettypeMatrixCriteria();
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
                    criteria.Entities.Add(new CSSportBettypeMatrixEntity
                    {
                        SportType = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString()),
                        BetType = HttpUtility.HtmlEncode(row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString()),
                        IsLive = HttpUtility.HtmlEncode(row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString()),
                        ProductId = HttpUtility.HtmlEncode(row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString()),
                        Enabled = HttpUtility.HtmlEncode(row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString()),
                        IsTest = HttpUtility.HtmlEncode(row.GetCell(5) == null ? string.Empty : row.GetCell(5).ToString()),
                        EventType = HttpUtility.HtmlEncode(row.GetCell(6) == null ? string.Empty : row.GetCell(6).ToString()),
                        SubProduct = HttpUtility.HtmlEncode(row.GetCell(7) == null ? string.Empty : row.GetCell(7).ToString()),
                        RefTable = HttpUtility.HtmlEncode(row.GetCell(8) == null ? string.Empty : row.GetCell(8).ToString()),
                        SportTypeGroupId = HttpUtility.HtmlEncode(row.GetCell(9) == null ? string.Empty : row.GetCell(9).ToString()),
                        BetTypeGroupId = HttpUtility.HtmlEncode(row.GetCell(10) == null ? string.Empty : row.GetCell(10).ToString())
                    });
                }

                criteria.UserID = AppSession.AccId;
                criteria.LoginIP = Utils.GetClientIP();
                result = this._manageCSSportBettypeMatrixDataAccess.Insert(criteria);
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

        public Result Update(CSSportBettypeMatrixEntity request) => _manageCSSportBettypeMatrixDataAccess.Update(new UpdateCSSportBettypeMatrixCriteria
        {
            Entities = new List<CSSportBettypeMatrixEntity> { request },
            LoginIP = Utils.GetClientIP(),
            UserID = AppSession.AccId
        });

        public Result Delete(DeleteCSSportBettypeMatrixRequest request) => _manageCSSportBettypeMatrixDataAccess.Delete(new DeleteCSSportBettypeMatrixCriteria
        {
            BetType = request.BetType,
            IsLive = request.IsLive,
            SportType = request.SportType,
            UserId = AppSession.AccId,
            LoginIp = Utils.GetClientIP()
        });
    }
}
