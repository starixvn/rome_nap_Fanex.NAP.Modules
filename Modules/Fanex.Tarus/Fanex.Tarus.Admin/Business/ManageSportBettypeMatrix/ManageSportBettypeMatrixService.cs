using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Fanex.Tarus.Admin.Criteria.ManageSportBettypeMatrix;
using Fanex.Tarus.Admin.DataAccess.ManageSportBettypeMatrix;
using Fanex.Tarus.Admin.Entities;
using Fanex.Tarus.Admin.Entities.ManageSportBettypeMatrix;
using Fanex.Tarus.Admin.Models.ManageSportBettypeMatrix;
using Fanex.Tarus.Infrastructure.Common;
using Fanex.Tarus.Infrastructure.Helper;
using NPOI.SS.UserModel;

namespace Fanex.Tarus.Admin.Business.ManageSportBettypeMatrix
{
    internal class ManageSportBettypeMatrixService
    {
        private readonly ManageSportBettypeMatrixDataAccess _manageSportBettypeMatrixDataAccess;

        public ManageSportBettypeMatrixService() : this(null)
        {
        }

        public ManageSportBettypeMatrixService(ManageSportBettypeMatrixDataAccess manageSportBettypeMatrixDataAccess)
        {
            _manageSportBettypeMatrixDataAccess = manageSportBettypeMatrixDataAccess ?? new ManageSportBettypeMatrixDataAccess();
        }

        public SportBettypeMatrixModel Select(SelectSportBettypeMatrixCriteria criteria) => _manageSportBettypeMatrixDataAccess.Select(criteria);

        public Result Insert(HttpPostedFileBase uploadFile)
        {
#pragma warning disable S1854 // Unused assignments should be removed
            var result = new Result();
#pragma warning restore S1854 // Unused assignments should be removed
            InsertSportBettypeMatrixCriteria criteria = new InsertSportBettypeMatrixCriteria();
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
                    criteria.Entities.Add(new SportBettypeMatrixEntity
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

                criteria.UserID = AppSession.AccId;
                criteria.LoginIP = Utils.GetClientIP();
                result = this._manageSportBettypeMatrixDataAccess.Insert(criteria);
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

        public Result Update(SportBettypeMatrixEntity request) => _manageSportBettypeMatrixDataAccess.Update(new UpdateSportBettypeMatrixCriteria
        {
            Entities = new List<SportBettypeMatrixEntity> { request }
        });
    }
}