namespace Fanex.Tarus.Excels.AutomateTransfer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using Resources;

    public class AutomateTransferExcel : IAutomateTransferExcel
    {
        private readonly ITarusExcel _tarusExcel;

        public AutomateTransferExcel(Stream stream)
        {
            _tarusExcel = new TarusExcel(stream, "AutomateTransfer");
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
        {
            var dt = CreateTemplateDataTable();
            var excelRows = _tarusExcel.Sheet.GetRowEnumerator();
            IRow excelRow;
            DataRow drow;
            ICell cell;
            int row = 0;

            while (excelRows.MoveNext())
            {
                excelRow = (HSSFRow)excelRows.Current;
                drow = dt.NewRow();
                //// Current Row Index
                var count = 0;
                var collectionColumn = 5;

                for (int i = 0, max = excelRow.LastCellNum; i < max; i++)
                {
                    cell = excelRow.GetCell(i);

                    if (cell != null)
                    {
                        switch (cell.CellType)
                        {
                            case CellType.Numeric:
                                drow[i + 1] = cell.NumericCellValue;
                                break;
                            default:
                                try
                                {
                                    if ((row > 0 && i == collectionColumn) &&
                                        !string.IsNullOrEmpty(cell.StringCellValue)
                                        && !string.Equals(cell.StringCellValue, "Yes", StringComparison.OrdinalIgnoreCase)
                                        && !string.Equals(cell.StringCellValue, "No", StringComparison.OrdinalIgnoreCase))
                                    {
                                        throw new Exception();
                                    }

                                    if (string.IsNullOrEmpty(cell.StringCellValue))
                                    {
                                        count++;
                                        break;
                                    }

                                    drow[i + 1] = cell.StringCellValue;
                                    break;
                                }
                                catch (Exception)
                                {
                                    throw new Exception("Input invalid at row " + (excelRow.RowNum + 1) + " cell " + (i + 1) + "!");
                                }
                        }
                    }
                    else
                    {
                        count++;
                    }
                }

                if (count != excelRow.LastCellNum && excelRow.RowNum > 0)
                {
                    drow[0] = excelRow.RowNum + 1;
                    dt.Rows.Add(drow);
                }

                row++;
            }

            return dt;
        }

        private DataTable CreateTemplateDataTable()
        {
            var dataTable = new DataTable();

            dataTable.Columns.AddRange(new[]
            {
                new DataColumn("No", typeof(int)),
                new DataColumn("From Cust", typeof(string)),
                new DataColumn("To Cust", typeof(string)),
                new DataColumn("Amount"),
                new DataColumn("ActualRate"),
                new DataColumn("Description", typeof(string)),
                new DataColumn("Collection", typeof(string)),
                new DataColumn("BU ID", typeof(string))
            });

            return dataTable;
        }

        private ExcelResult ValidateColumnNames(IRow headerRow)
        {
            if (headerRow == null || headerRow.LastCellNum < 7)
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

            string amount = headerRow.Cells[2].ToString();

            if (string.IsNullOrEmpty(amount) ||
                !amount.Equals("Amount", StringComparison.Ordinal))
            {
                invalidColumnNames.Add(amount);
            }

            string actualRate = headerRow.Cells[3].ToString();

            if (string.IsNullOrEmpty(actualRate) ||
                !actualRate.Equals("ActualRate", StringComparison.Ordinal))
            {
                invalidColumnNames.Add(actualRate);
            }

            string description = headerRow.Cells[4].ToString();

            if (string.IsNullOrEmpty(description) ||
                !description.Equals("Description", StringComparison.Ordinal))
            {
                invalidColumnNames.Add(description);
            }

            string collection = headerRow.Cells[5].ToString();

            if (string.IsNullOrEmpty(collection) ||
                !collection.Equals("Collection", StringComparison.Ordinal))
            {
                invalidColumnNames.Add(collection);
            }

            string BUId = headerRow.Cells[6].ToString();

            if (string.IsNullOrEmpty(BUId) ||
                !BUId.Equals("BU ID", StringComparison.Ordinal))
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
