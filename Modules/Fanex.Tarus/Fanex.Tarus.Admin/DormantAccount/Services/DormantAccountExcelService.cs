using System;
using System.Collections.Generic;
using System.Drawing;
using ExcelUtil.Spreadsheet;
using Fanex.Tarus.Admin.DormantAccount.Models;
using Fanex.Tarus.Admin.DormantAccount.Request;
using Fanex.Tarus.Admin.Resource;
using Sunplus.Accounting.Core.Helper;

namespace Fanex.Tarus.Admin.DormantAccount.Services
{
    public interface IDormantAccountExcelService
    {
        ExcelFile ExportExcelByDateType(SelectDormantAccountRequest request);

        ExcelFile ExportExcelByUsername(SelectDormantAccountRequest request);

        ExcelFile ExportExcelDrilldown(SelectDormantAccountRequest request);
    }

    public class DormantAccountExcelService : IDormantAccountExcelService
    {
        private readonly IDormantAccountService service;
        private int startRow = 0;
        private const int NormalFontSize = 190;
        private const int HeaderFontSize = 250;

        public DormantAccountExcelService()
        {
            service = new DormantAccountService();
        }

        public ExcelFile ExportExcelByUsername(SelectDormantAccountRequest request)
        {
            var excelFile = new ExcelFile();
            var viewModel = service.GetByUsername(request);
            var worksheet = excelFile.Worksheets.Add("DormantAccount");

            ExportHeaderFile(ref worksheet, viewByDateType: false, request: request);
            ExportExcelFileAssignHeader(ref worksheet, viewByDateType: false);
            ExportExcelFileAssignBodyForUsername(ref worksheet, viewModel.DataByUsername);

            return excelFile;
        }

        public ExcelFile ExportExcelByDateType(SelectDormantAccountRequest request)
        {
            var excelFile = new ExcelFile();
            var viewModel = service.GetByDateType(request);
            var worksheet = excelFile.Worksheets.Add("DormantAccount");

            ExportHeaderFile(ref worksheet, viewByDateType: true, request: request);
            ExportExcelFileAssignHeader(ref worksheet, viewByDateType: true);
            ExportExcelFileAssignBodyForDateType(ref worksheet, viewModel.DataByDateType);

            return excelFile;
        }

        public ExcelFile ExportExcelDrilldown(SelectDormantAccountRequest request)
        {
            var excelFile = new ExcelFile();
            var viewModel = service.DrillDownByDateType(request);
            var worksheet = excelFile.Worksheets.Add("DormantAccount");

            ExportHeaderFile(ref worksheet, viewByDateType: false, request: request);
            ExportExcelFileAssignHeader(ref worksheet, viewByDateType: false);
            ExportExcelFileAssignBodyForUsername(ref worksheet, viewModel.DataByUsername);

            return excelFile;
        }

        private void ExportHeaderFile(ref ExcelWorksheet worksheet, bool viewByDateType, SelectDormantAccountRequest request)
        {
            var headerFormat = new ExcelGemboxUtils().GetStyleText(false);
            var filterFormat = new ExcelGemboxUtils().GetStyleText(false);
            headerFormat.Font.Size = HeaderFontSize;
            headerFormat.Font.Weight = ExcelFont.BoldWeight;
            headerFormat.HorizontalAlignment = HorizontalAlignmentStyle.Left;
            filterFormat.HorizontalAlignment = HorizontalAlignmentStyle.Left;
            filterFormat.Font.Size = NormalFontSize;
            if (viewByDateType)
            {
                var excelCell = worksheet.Cells.GetSubrangeAbsolute(startRow, 0, startRow, 26);
                excelCell.Merged = true;
                excelCell.Style = headerFormat;
                excelCell.Value = "Dormant SMA Account List";
                worksheet.Rows[startRow].Height = 255 *2;
                startRow++;

                excelCell = worksheet.Cells.GetSubrangeAbsolute(startRow, 0, startRow, 26);
                excelCell.Merged = true;
                excelCell.Style = filterFormat;
                excelCell.Value = $"Archive Date: {request.FromDateText} - {request.ToDateText}";

                startRow += 2;
            }
            else
            {
                var excelCell = worksheet.Cells.GetSubrangeAbsolute(startRow, 0, startRow, 12);
                excelCell.Merged = true;
                excelCell.Style = headerFormat;
                excelCell.Value = $"Dormant Account List {DateTime.Today.ToString("MM/dd/yyyy")}";
                worksheet.Rows[startRow].Height = 255*2;
                startRow += 2;
            }

        }

        private void ExportExcelFileAssignHeader(ref ExcelWorksheet worksheet, bool viewByDateType)
        {
            var startCol = 0;
            var headerFormat = new ExcelGemboxUtils().GetStyleTitle(Color.FromArgb(205, 222, 255));
            headerFormat.Font.Size = NormalFontSize;
            var headerArchiveFormat = new ExcelGemboxUtils().GetStyleTitle(Color.FromArgb(119, 188, 101));
            headerArchiveFormat.Font.Size = NormalFontSize;

            ExcelCell excelCell;

            excelCell = worksheet.Cells[startRow, startCol];
            excelCell.Style = headerFormat;
            excelCell.Value = "#";
            worksheet.Columns[startCol++].Width = 255 * 5;
            if (viewByDateType)
            {
                excelCell = worksheet.Cells[startRow, startCol];
                excelCell.Style = headerFormat;
                excelCell.Value = Language.GroupHeadline;
                worksheet.Columns[startCol++].Width = 255 * 25;

                excelCell = worksheet.Cells[startRow, startCol];
                excelCell.Style = headerFormat;
                excelCell.Value = Language.HeadlineType;
                worksheet.Columns[startCol++].Width = 255 * 15;

                excelCell = worksheet.Cells[startRow, startCol];
                excelCell.Style = headerFormat;
                excelCell.Value = Language.DirectAccount;
                worksheet.Columns[startCol++].Width = 255 * 25;
            }
            else
            {
                excelCell = worksheet.Cells[startRow, startCol];
                excelCell.Style = headerFormat;
                excelCell.Value = Language.Username;
                worksheet.Columns[startCol++].Width = 255 * 25;
            }

            excelCell = worksheet.Cells[startRow, startCol];
            excelCell.Style = headerFormat;
            excelCell.Value = Language.Level;
            worksheet.Columns[startCol++].Width = 255 * 10;

            excelCell = worksheet.Cells[startRow, startCol];
            excelCell.Style = headerFormat;
            excelCell.Value = Language.Status;
            worksheet.Columns[startCol++].Width = 255 * 10;

            excelCell = worksheet.Cells[startRow, startCol];
            excelCell.Style = headerFormat;
            excelCell.Value = Language.ArchiveStage;
            worksheet.Columns[startCol++].Width = 255 * 15;

            excelCell = worksheet.Cells[startRow, startCol];
            excelCell.Style = headerFormat;
            excelCell.Value = Language.TobeClearedGivenCredit;
            worksheet.Columns[startCol++].Width = 255 * 15;
            worksheet.Rows[startRow].Height = 255 *3;

            excelCell = worksheet.Cells[startRow, startCol];
            excelCell.Style = headerFormat;
            excelCell.Value = Language.ClearedGivenCredit;
            worksheet.Columns[startCol++].Width = 255 * 15;

            excelCell = worksheet.Cells[startRow, startCol];
            excelCell.Style = headerFormat;
            excelCell.Value = Language.YesterdayBalance;
            worksheet.Columns[startCol++].Width = 255 * 15;

            excelCell = worksheet.Cells[startRow, startCol];
            excelCell.Style = headerFormat;
            excelCell.Value = Language.LastLogin;
            worksheet.Columns[startCol++].Width = 255 * 15;

            excelCell = worksheet.Cells[startRow, startCol];
            excelCell.Style = headerFormat;
            excelCell.Value = Language.ArchiveDateClose;
            worksheet.Columns[startCol++].Width = 255 * 15;

            excelCell = worksheet.Cells[startRow, startCol];
            excelCell.Style = headerFormat;
            excelCell.Value = Language.ArchiveDateDisable;
            worksheet.Columns[startCol++].Width = 255 * 15;

            excelCell = worksheet.Cells[startRow, startCol];
            excelCell.Style = headerFormat;
            excelCell.Value = Language.ArchiveDateDelSetting;
            worksheet.Columns[startCol++].Width = 255 * 15;

            excelCell = worksheet.Cells[startRow, startCol];
            excelCell.Style = headerFormat;
            excelCell.Value = Language.ArchiveDateDelAccount;
            worksheet.Columns[startCol++].Width = 255 * 15;
            if (viewByDateType)
            {
                var excelCellMergered = worksheet.Cells.GetSubrangeAbsolute(startRow, startCol, startRow, startCol + 3);
                excelCellMergered.Style = headerArchiveFormat;
                excelCellMergered.Merged = true;
                excelCellMergered.Value = Language.TotalArchivedMaster;
                startCol += 4;

                excelCellMergered = worksheet.Cells.GetSubrangeAbsolute(startRow, startCol, startRow, startCol + 3);
                excelCellMergered.Style = headerArchiveFormat;
                excelCellMergered.Merged = true;
                excelCellMergered.Value = Language.TotalArchivedAgent;
                startCol += 4;

                excelCellMergered = worksheet.Cells.GetSubrangeAbsolute(startRow, startCol, startRow, startCol + 3);
                excelCellMergered.Style = headerArchiveFormat;
                excelCellMergered.Merged = true;
                excelCellMergered.Value = Language.TotalArchivedMember;
            }
            startRow++;
        }

        private void ExportExcelFileAssignBodyForDateType(
            ref ExcelWorksheet worksheet,
            IEnumerable<DormantDateTypeModel> accounts)
        {
            CellStyle cellStyleNormal = new ExcelGemboxUtils().GetStyleTextCenter(false);
            CellStyle cellStyleWaring = new ExcelGemboxUtils().GetStyleText(Color.Red, 255, 0, 0, HorizontalAlignmentStyle.Center);
            cellStyleNormal.Font.Size = NormalFontSize;
            cellStyleWaring.Font.Size = NormalFontSize;

            ExcelCell excelCell;
            foreach (var account in accounts)
            {
                var startCol = 0;
                var backgroundColor = account.RowNumber == 0 ? Color.FromArgb(247, 150, 70) : Color.FromArgb(255, 255, 255);
                var cellStyle = cellStyleNormal;
                if (account.IsWarningTicket)
                    cellStyle = cellStyleWaring;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.RowNumber == 0 ? string.Empty : account.RowNumber.ToString();

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
                excelCell.Value = account.HeadlineGroup;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.HeadlineTypeText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
                excelCell.Value = account.Username;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.RoleText;

                var status = account.IsWarningTicket ? $"{account.StatusText}*" : account.StatusText;
                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = status;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.StageText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
                excelCell.Value = account.ToBeClearedGivenCreditText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
                excelCell.Value = account.ClearedGivenCreditText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
                excelCell.Value = account.YesterdayBalanceText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.LastLoggedText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.ClosedDateText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.DisabledDateText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.SettingDeletedDateText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.AccountDeletedDateText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = $"{account.ArchivedMaster.Close}";

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = $"{account.ArchivedMaster.Disable}";

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = $"{account.ArchivedMaster.DelSetting}";

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = $"{account.ArchivedMaster.DelAccount}";


                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = $"{account.ArchivedAgent.Close}";

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = $"{account.ArchivedAgent.Disable}";

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = $"{account.ArchivedAgent.DelSetting}";

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = $"{account.ArchivedAgent.DelAccount}";

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = $"{account.ArchivedMember.Close}";

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = $"{account.ArchivedMember.Disable}";

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = $"{account.ArchivedMember.DelSetting}";

                excelCell = worksheet.Cells[startRow, startCol];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = $"{account.ArchivedMember.DelAccount}";

                startRow++;
            }
        }

        private void ExportExcelFileAssignBodyForUsername(
            ref ExcelWorksheet worksheet,
            IEnumerable<DormantUsernameModel> accounts)
        {
            CellStyle cellStyleNormal = new ExcelGemboxUtils().GetStyleTextCenter(false);
            CellStyle cellStyleWaring = new ExcelGemboxUtils().GetStyleText(Color.Red, 255, 0, 0, HorizontalAlignmentStyle.Center);
            cellStyleWaring.Font.Size = NormalFontSize;
            cellStyleNormal.Font.Size = NormalFontSize;

            ExcelCell excelCell;

            foreach (var account in accounts)
            {
                var startCol = 0;
                var backgroundColor = account.RowNumber == 0 ? Color.FromArgb(247, 150, 70) : Color.FromArgb(255, 255, 255);
                var cellStyle = cellStyleNormal;
                if (account.IsWarningTicket)
                    cellStyle = cellStyleWaring;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.RowNumber == 0 ? string.Empty : account.RowNumber.ToString();

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
                excelCell.Value = account.Username;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.RoleText;

                var status = account.IsWarningTicket ? $"{account.StatusText}*" : account.StatusText;
                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = status;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.StageText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
                excelCell.Value = account.ToBeClearedGivenCreditText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
                excelCell.Value = account.ClearedGivenCreditText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
                excelCell.Value = account.YesterdayBalanceText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.LastLoggedText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.ClosedDateText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.DisabledDateText;

                excelCell = worksheet.Cells[startRow, startCol++];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.SettingDeletedDateText;

                excelCell = worksheet.Cells[startRow, startCol];
                excelCell.Style = cellStyle;
                excelCell.Style.FillPattern.SetSolid(backgroundColor);
                excelCell.Value = account.AccountDeletedDateText;

                startRow++;
            }
        }
    }
}
