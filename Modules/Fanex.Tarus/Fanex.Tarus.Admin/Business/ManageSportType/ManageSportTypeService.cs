namespace Fanex.Tarus.Admin.Business.ManageSportType
{
    using Fanex.Tarus.Admin.Criteria.ManageSporttype;
    using Fanex.Tarus.Admin.DataAccess.ManageSporttype;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.ManageSporttype;
    using Fanex.Tarus.Admin.Models.ManageSporttype;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;
    using NPOI.SS.UserModel;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;

    public class ManageSportTypeService
    {
        private readonly ManageSporttypeDataAccess _manageSportTypeDataAccess;

        public ManageSportTypeService() : this(null) { }

        public ManageSportTypeService(ManageSporttypeDataAccess manageSporttypeDataAccess)
        {
            _manageSportTypeDataAccess = manageSporttypeDataAccess ?? new ManageSporttypeDataAccess();
        }

        public SportTypeModel Select(SelectSportTypeCriteria criteria) => _manageSportTypeDataAccess.Select(criteria);

        public Result Insert(HttpPostedFileBase uploadFile)
        {
#pragma warning disable S1854 // Unused assignments should be removed
            var result = new Result();
#pragma warning restore S1854 // Unused assignments should be removed
            InsertSportTypeCriteria criteria = new InsertSportTypeCriteria();
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
                    criteria.Entities.Add(new Entities.ManageSporttype.SportTypeEntity
                    {
                        SportType = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString()),
                        SportName = HttpUtility.HtmlEncode(row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString()),
                        SportNameCh = HttpUtility.HtmlEncode(row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString()),
                        SportNameCs = HttpUtility.HtmlEncode(row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString()),
                        SportNameJp = HttpUtility.HtmlEncode(row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString()),
                        SportNameIt = HttpUtility.HtmlEncode(row.GetCell(5) == null ? string.Empty : row.GetCell(5).ToString()),
                        SportNameTh = HttpUtility.HtmlEncode(row.GetCell(6) == null ? string.Empty : row.GetCell(6).ToString()),
                        ParlayMaxPayout = HttpUtility.HtmlEncode(row.GetCell(7) == null ? string.Empty : row.GetCell(7).ToString()),
                        SportNameKo = HttpUtility.HtmlEncode(row.GetCell(8) == null ? string.Empty : row.GetCell(8).ToString()),
                        SportNameES = HttpUtility.HtmlEncode(row.GetCell(9) == null ? string.Empty : row.GetCell(9).ToString()),
                        SportNameVN = HttpUtility.HtmlEncode(row.GetCell(10) == null ? string.Empty : row.GetCell(10).ToString()),
                        SportNameEL = HttpUtility.HtmlEncode(row.GetCell(11) == null ? string.Empty : row.GetCell(11).ToString()),
                        SportNameRU = HttpUtility.HtmlEncode(row.GetCell(12) == null ? string.Empty : row.GetCell(12).ToString()),
                        SportNameID = HttpUtility.HtmlEncode(row.GetCell(13) == null ? string.Empty : row.GetCell(13).ToString()),
                        SportNamePT = HttpUtility.HtmlEncode(row.GetCell(14) == null ? string.Empty : row.GetCell(14).ToString()),
                        SportNameFR = HttpUtility.HtmlEncode(row.GetCell(15) == null ? string.Empty : row.GetCell(15).ToString()),
                        SportNameMMR = HttpUtility.HtmlEncode(row.GetCell(16) == null ? string.Empty : row.GetCell(16).ToString()),
                        SportNamePL = HttpUtility.HtmlEncode(row.GetCell(17) == null ? string.Empty : row.GetCell(17).ToString()),
                        SportNameHI = HttpUtility.HtmlEncode(row.GetCell(18) == null ? string.Empty : row.GetCell(18).ToString())
                    });
                }

                criteria.UserID = AppSession.AccId;
                criteria.LoginIP = Utils.GetClientIP();
                result = this._manageSportTypeDataAccess.Insert(criteria);
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

        public Result Update(SportTypeEntity request) => _manageSportTypeDataAccess.Update(new UpdateSportTypeCriteria
        {
            Entities = new List<SportTypeEntity> { request }
        });
    }
}
