namespace Fanex.Tarus.ExchangeRate.Helpers
{
    using System.Collections.Generic;
    using NPOI.SS.UserModel;
    using Sunplus.Accounting.Entities;
    using Models;

    public static class ExchangeRateHelper
    {
        public static List<string> CheckRow(ref List<USDTRate> rawData, IRow row, ref bool validateSuccess)
        {
            var errMss = new List<string>();
            var cellOne = row.GetCell(0);
            var cellTwo = row.GetCell(1);

            CheckRequiredField(cellOne, "The Currency Code is required field.", ref errMss);
            CheckRequiredField(cellOne, "The USDT/Unit is required field.", ref errMss);
            CheckDuplicateCurrency(cellOne, ref rawData, ref errMss);

            if (!IsNumricCell(cellTwo))
            {
                errMss.Add("Invalid USDT rate");
            }

            if (errMss.Count > 0)
            {
                validateSuccess = false;
            }
            return errMss;
        }

        public static void CheckRequiredField(ICell cell, string message, ref List<string> messageContainer)
        {
            if (cell == null || string.IsNullOrWhiteSpace(cell.ToString()))
            {
                messageContainer.Add(message);
            }
        }

        public static void CheckDuplicateCurrency(ICell cell, ref List<USDTRate> rawData,
            ref List<string> messageContainer)
        {
            var duplicateOtherCurrency = rawData.FindAll(e =>
                e.CurrencyCode == cell.ToString() && !e.RemarkErrors.Exists(mes => mes == "Currency is duplicated"));
            if (duplicateOtherCurrency.Count <= 0) return;
            foreach (var duplicateItem in duplicateOtherCurrency)
            {
                duplicateItem.RemarkErrors.Add("Currency is duplicated");
            }

            messageContainer.Add("Currency is duplicated");
        }

        private static bool IsNumricCell(ICell cell)
        {
            try
            {
                return double.TryParse(cell.ToString(), out var b);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsHaveDataRow(IRow row)
        {
            for (int i = 0; i <= 9; i++)
            {
                if (row.GetCell(i) != null && !string.IsNullOrWhiteSpace(row.GetCell(i).ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        public static Result CheckRequiredField(IRow row, int rowCount)
        {
            var cellOne = row.GetCell(1).GetType();
            var cellTwo = row.GetCell(1).GetType();
            if (string.IsNullOrWhiteSpace(cellOne.ToString()))
            {
                return new Result(1, "Row " + rowCount.ToString() + ": The Currency Code is required field.");
            }

            if (string.IsNullOrWhiteSpace(cellTwo.ToString()))
            {
                return new Result(1, "Row " + rowCount.ToString() + ": The USDT/Unit is required field.");
            }

            return null;
        }

        public static Result CheckHeaderName(IRow row)
        {
            var cellOne = row.GetCell(0);
            var cellTwo = row.GetCell(1);
            if (cellOne.ToString().ToLower() != "currency code")
            {
                return new Result(1, "Invalid template.");
            }

            if (cellTwo.ToString().ToLower() != "usdt/unit")
            {
                return new Result(1, "Invalid template.");
            }

            return null;
        }
    }
}