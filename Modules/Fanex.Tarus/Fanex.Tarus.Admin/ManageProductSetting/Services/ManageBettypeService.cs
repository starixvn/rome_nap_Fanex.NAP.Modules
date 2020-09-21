namespace Fanex.Tarus.Admin.ManageProductSetting.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageBettype;
    using Fanex.Tarus.Admin.ManageProductSetting.DataAccess;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using NPOI.SS.UserModel;

    public class ManageBettypeService
    {
        private readonly IManageBettypeDataAccess _manageBettypeDataAccess;

        public ManageBettypeService() : this(null)
        {
        }

        public ManageBettypeService(IManageBettypeDataAccess manageBettypeDataAccess)
        {
            _manageBettypeDataAccess = manageBettypeDataAccess ?? new ManageBettypeDataAccess();
        }

        public BettypeModel Select(SelectBettypeCriteria criteria) => _manageBettypeDataAccess.Select(criteria);

        public Result Insert(HttpPostedFileBase uploadFile)
        {
#pragma warning disable S1854 // Unused assignments should be removed
            var result = new Result();
#pragma warning restore S1854 // Unused assignments should be removed
            InsertBettypeCriteria criteria = new InsertBettypeCriteria();
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

                    criteria.Entities.Add(new BettypeEntity
                    {
                        TypeId = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString()),
                        TypeNamee = HttpUtility.HtmlEncode(row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString()),
                        TypeNamec = HttpUtility.HtmlEncode(row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString()),
                        TypeDesc = HttpUtility.HtmlEncode(row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString()),
                        TypeNamec_uni = HttpUtility.HtmlEncode(row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString()),
                        TypeNamej = HttpUtility.HtmlEncode(row.GetCell(5) == null ? string.Empty : row.GetCell(5).ToString()),
                        TypeNamcn = HttpUtility.HtmlEncode(row.GetCell(6) == null ? string.Empty : row.GetCell(6).ToString()),
                        TypeNamecn = HttpUtility.HtmlEncode(row.GetCell(7) == null ? string.Empty : row.GetCell(7).ToString()),
                        TypeNameth = HttpUtility.HtmlEncode(row.GetCell(8) == null ? string.Empty : row.GetCell(8).ToString()),
                        TypeNameit = HttpUtility.HtmlEncode(row.GetCell(9) == null ? string.Empty : row.GetCell(9).ToString()),
                        TypeNameko = HttpUtility.HtmlEncode(row.GetCell(10) == null ? string.Empty : row.GetCell(10).ToString()),
                        TypeNameES = HttpUtility.HtmlEncode(row.GetCell(11) == null ? string.Empty : row.GetCell(11).ToString()),
                        TypeNameVN = HttpUtility.HtmlEncode(row.GetCell(12) == null ? string.Empty : row.GetCell(12).ToString()),
                        TypeNameEL = HttpUtility.HtmlEncode(row.GetCell(13) == null ? string.Empty : row.GetCell(13).ToString()),
                        TypeNamee_Eu = HttpUtility.HtmlEncode(row.GetCell(14) == null ? string.Empty : row.GetCell(14).ToString()),
                        TypeNameRU = HttpUtility.HtmlEncode(row.GetCell(15) == null ? string.Empty : row.GetCell(15).ToString()),
                        TypeNameID = HttpUtility.HtmlEncode(row.GetCell(16) == null ? string.Empty : row.GetCell(16).ToString()),
                        TypeNamePT = HttpUtility.HtmlEncode(row.GetCell(17) == null ? string.Empty : row.GetCell(17).ToString()),
                        TypeNameFR = HttpUtility.HtmlEncode(row.GetCell(18) == null ? string.Empty : row.GetCell(18).ToString()),
                        ParentTypeid = HttpUtility.HtmlEncode(row.GetCell(19) == null ? string.Empty : row.GetCell(19).ToString()),
                        TypeNameMMR = HttpUtility.HtmlEncode(row.GetCell(20) == null ? string.Empty : row.GetCell(20).ToString()),
                        TypeNameHI = HttpUtility.HtmlEncode(row.GetCell(21) == null ? string.Empty : row.GetCell(21).ToString()),
                        DisplayedTypeID = HttpUtility.HtmlEncode(row.GetCell(22) == null ? string.Empty : row.GetCell(22).ToString()),
                        LicSpreadBetTypeGroupId = HttpUtility.HtmlEncode(row.GetCell(23) == null ? string.Empty : row.GetCell(23).ToString())
                    });
                }

                result = this._manageBettypeDataAccess.Insert(criteria);
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

        public Result Update(BettypeEntity request) => _manageBettypeDataAccess.Update(new UpdateBettypeCriteria
        {
            Entities = new List<BettypeEntity> { request }
        });
    }
}