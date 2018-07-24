namespace Fanex.Tarus.Excels.VirtualRate
{
    using System;
    using System.Data;
    using System.IO;
    using NPOI.SS.UserModel;
    using Resources;

    public class VirtualRateExcel : IVirtualRateExcel
    {
        private readonly ITarusExcel _tarusExcel;

        public VirtualRateExcel(string filePath)
        {
            _tarusExcel = new TarusExcel(filePath, "ExportMultiStatement");
        }

        public VirtualRateExcel(Stream stream)
        {
            _tarusExcel = new TarusExcel(stream, "ExportMultiStatement");
        }

        public ExcelResult ValidateTemplateFile()
        {
            if (_tarusExcel.Sheet == null)
            {
                return CreateErrorResult(Labels.InvalidSheetName);
            }

            if (_tarusExcel.FirstRow == _tarusExcel.LastRow)
            {
                return CreateErrorResult(Labels.EmptyData);
            }

            return ValidateColumnNames(_tarusExcel.Sheet.GetRow(_tarusExcel.FirstRow));
        }

        public DataTable ExcelToDataTable()
            => _tarusExcel.ExcelToDataTable();

        private ExcelResult ValidateColumnNames(IRow headerRow)
        {
            if (headerRow == null || headerRow.LastCellNum < 1)
            {
                return CreateErrorResult(Labels.InvalidExcelTemplate);
            }

            string username = headerRow.Cells[0].ToString();

            if (string.IsNullOrEmpty(username) ||
                !username.Equals("Username", StringComparison.Ordinal))
            {
                return CreateErrorResult($"Invalid column name at row 1 cell {username}.");
            }

            return new ExcelResult();
        }

        private static ExcelResult CreateErrorResult(string message)
            => new ExcelResult(1, message);
    }
}
