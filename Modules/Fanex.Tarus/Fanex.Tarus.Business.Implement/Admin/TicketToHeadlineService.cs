using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Fanex.Tarus.Business.Interface.Admin;
using Fanex.Tarus.DataAccess.Implement.Admin;
using Fanex.Tarus.DataAccess.Interface.Admin;
using Fanex.Tarus.Entities.Criteria;
using Fanex.Tarus.Entities.Criteria.Admin;
using Fanex.Tarus.Web.Models.Admin;
using NPOI.SS.UserModel;

namespace Fanex.Tarus.Business.Implement.Admin
{
    internal enum ErrorCode
    {
        ActualAndWrongResultAreRequired = 101
    }

    public class TicketToHeadlineService : ITicketToHeadlineService
    {
        private readonly ITicketToHeadlineRepository _ticketToHeadlineRepository;

        private readonly string _cacheGetDataCriteria = "TicketToHeadlineCritaria";

        private readonly int[] SupportedNotifyHeadlineProducts = { 1, 3, 5, 27 };

        public TicketToHeadlineService()
        {
            this._ticketToHeadlineRepository = new TicketToHeadlineRepository();
        }

        public List<SelectListItem> GetSuportedProduct(GetTicketToHeadlineSupportedProductCriteria criteria)
        {
            return this._ticketToHeadlineRepository.GetSuportedProduct(criteria);
        }

        public TicketToHeadlineViewModel GetData(HttpPostedFileBase uploadFile, TicketToHeadlineCriteria criteria)
        {
            TicketToHeadlineViewModel result;
            int rowCount = 1;
            StringBuilder xmlParam = new StringBuilder("<ROOT>");
            try
            {
                IWorkbook workbook = WorkbookFactory.Create(uploadFile.InputStream);
                ISheet sheet = workbook.GetSheetAt(0);
                RemoveLastEmptyRows(sheet);
                var lastRowNum = sheet.LastRowNum;

                for (int i = 1; i <= lastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    string userName = HttpUtility.HtmlEncode(row?.GetCell(0) == null ? string.Empty : row?.GetCell(0).ToString());
                    string refno = HttpUtility.HtmlEncode(row?.GetCell(1) == null ? string.Empty : row?.GetCell(1).ToString());
                    xmlParam.Append($"<File UserName=\"{userName.Trim()}\" RefNo=\"{refno.Trim()}\" />");
                    if (rowCount == 2)
                    {
                        string matchId = HttpUtility.HtmlEncode(row?.GetCell(2) == null ? string.Empty : row?.GetCell(2).ToString());
                        string messageId = HttpUtility.HtmlEncode(row?.GetCell(5) == null ? string.Empty : row?.GetCell(5).ToString());
                        if (SupportedNotifyHeadlineProducts.Contains(criteria.ProductId))
                        {
                            criteria.MatchId = string.IsNullOrWhiteSpace(matchId) ? 0 : Convert.ToInt32(matchId);
                            criteria.MessageId = string.IsNullOrWhiteSpace(messageId) ? 0 : Convert.ToInt32(messageId);
                        }

                        criteria.ActualResult = HttpUtility.HtmlEncode(row?.GetCell(3) == null ? string.Empty : row?.GetCell(3).ToString());
                        criteria.WrongResult = HttpUtility.HtmlEncode(row?.GetCell(4) == null ? string.Empty : row?.GetCell(4).ToString());
                    }
                }

                xmlParam.Append("</ROOT>");
                criteria.File = xmlParam.ToString();

            }
            catch (Exception ex)
            {
                Logging.Logger.Log.Error("Invalid file uploaded. Error at line: " + rowCount.ToString(), ex);
                return new TicketToHeadlineViewModel { ErrorCode = 500, ErrorMessage = "Invalid file uploaded." };
            }

            result = this._ticketToHeadlineRepository.GetData(criteria);
            HttpContext.Current.Session[_cacheGetDataCriteria] = result.ErrorCode == 0 ? new TicketToHeadlineCached(criteria) : null;

            return result;
        }

        public TicketToHeadlineViewModel NotifyHeadline()
        {
            if (HttpContext.Current.Session[_cacheGetDataCriteria] == null || HttpContext.Current.Session[_cacheGetDataCriteria].GetType() != typeof(TicketToHeadlineCached))
            {
                throw new InvalidOperationException("TicketToHeadlineCriteria in Session can not be null. Must Submit before Notify Headline.");
            }

            var criteria = new TicketToHeadlineCriteria((TicketToHeadlineCached)HttpContext.Current.Session[_cacheGetDataCriteria]);
            return this._ticketToHeadlineRepository.GetData(criteria, true);
        }

        public IWorkbook ExportExcel(TicketToHeadlineViewModel data)
        {
            TicketToHeadlineExcelService excelService = new TicketToHeadlineExcelService(data);
            return excelService.Export();
        }


        public TicketToHeadlineViewModel GetFakeData(int? pageSize, int? pageIndex, int rowNo = 400)
        {
            return _ticketToHeadlineRepository.GetFakeData(pageSize, pageIndex, rowNo);
        }

        private void RemoveLastEmptyRows(ISheet sheet)
        {
            var lastRowNum = sheet.LastRowNum;
            for (int i = lastRowNum; i >= 0; i--)
            {
                var row = sheet.GetRow(i);
                if (row != null)
                {
                    var lastCellNum = row.LastCellNum;
                    bool isAllCellEmpty = true;

                    for (int j = 0; j <= lastCellNum; j++)
                    {
                        var cellValue = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString());
                        if (!string.IsNullOrWhiteSpace(cellValue))
                        {
                            isAllCellEmpty = false;
                            break;
                        }
                    }

                    if (isAllCellEmpty)
                    {
                        sheet.RemoveRow(row);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
