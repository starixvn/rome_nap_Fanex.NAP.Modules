namespace Fanex.Tarus.Excels.CrossCurrencyTransfer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using NPOI.SS.UserModel;
    using Resources;

    public class CrossCurrencyTransferExcel : ICrossCurrencyTransferExcel
    {
        private readonly ITarusExcel _tarusExcel;

        public CrossCurrencyTransferExcel(string filePath)
        {
            _tarusExcel = new TarusExcel(filePath, "CrossCurrencyTransfer");
        }

        public CrossCurrencyTransferExcel(Stream stream)
        {
            _tarusExcel = new TarusExcel(stream, "CrossCurrencyTransfer");
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
            if (headerRow == null || headerRow.LastCellNum < 6)
            {
                return CreateErrorResult(Labels.InvalidExcelTemplate);
            }

            IList<string> invalidColumnNames = new List<string>();
            string fromCust = headerRow.Cells[0].ToString();

            if (string.IsNullOrEmpty(fromCust) ||
                !fromCust.Equals("From Cust", StringComparison.Ordinal))
            {
                invalidColumnNames.Add(fromCust);
            }

            string toCust = headerRow.Cells[1].ToString();

            if (string.IsNullOrEmpty(toCust) ||
                !toCust.Equals("To Cust", StringComparison.Ordinal))
            {
                invalidColumnNames.Add(toCust);
            }

            string fromAmount = headerRow.Cells[2].ToString();

            if (string.IsNullOrEmpty(fromAmount) ||
                !fromAmount.Equals("From Amount", StringComparison.Ordinal))
            {
                invalidColumnNames.Add(fromAmount);
            }

            string toAmount = headerRow.Cells[3].ToString();

            if (string.IsNullOrEmpty(toAmount) ||
                !toAmount.Equals("To Amount", StringComparison.Ordinal))
            {
                invalidColumnNames.Add(toAmount);
            }

            string remark = headerRow.Cells[5].ToString();

            if (string.IsNullOrEmpty(remark) ||
                !remark.Equals("Remark", StringComparison.Ordinal))
            {
                invalidColumnNames.Add(remark);
            }

            string collection = headerRow.Cells[6].ToString();

            if (string.IsNullOrEmpty(collection) ||
                !collection.Equals("Collection", StringComparison.Ordinal))
            {
                invalidColumnNames.Add(collection);
            }

            if (invalidColumnNames.Count > 0)
            {
                return CreateErrorResult(string.Format(Labels.InvalidColumnName, string.Join(", ", invalidColumnNames)));
            }

            return new ExcelResult();
        }

        private static ExcelResult CreateErrorResult(string message)
            => new ExcelResult(1, message);
    }
}
