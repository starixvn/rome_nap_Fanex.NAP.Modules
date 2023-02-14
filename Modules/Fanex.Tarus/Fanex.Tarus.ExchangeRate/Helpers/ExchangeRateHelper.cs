namespace Fanex.Tarus.ExchangeRate.Helpers
{
    using System.Collections.Generic;
    using NPOI.SS.UserModel;
    using Sunplus.Accounting.Entities;
    using Models;
    using System;
    public static class ExchangeRateHelper
    {
        public static List<string> CheckRow(ref List<USDTRate> rawData, IRow row, ref bool validateSuccess)
        {
            var errMss = new List<string>();
            var cellOne = row.GetCell(0);
            var cellTwo = row.GetCell(1);

            CheckCurrencyCode(cellOne, ref rawData, ref errMss);
            CheckUSDTRate(cellTwo, ref errMss);

            if (errMss.Count > 0)
            {
                validateSuccess = false;
            }
            
            return errMss;
        }
        
        private static void CheckCurrencyCode(ICell cell,ref List<USDTRate> rawData, ref List<string> messageContainer)
        {
            if (cell == null || string.IsNullOrWhiteSpace(cell.ToString()))
            {
                messageContainer.Add("Invalid currency");
            }
            else
            {
                CheckDuplicateCurrency(cell, ref rawData, ref messageContainer);
            }
        }

        private static void CheckUSDTRate(ICell cell, ref List<string> messageContainer)
        {
            if (cell == null || string.IsNullOrWhiteSpace(cell.ToString()) || !IsPositiveCell(cell))
            {
                messageContainer.Add("Invalid USDT rate");
            }
        }
        
        private static void CheckDuplicateCurrency(ICell cell, ref List<USDTRate> rawData, ref List<string> messageContainer)
        {
            if (cell == null || string.IsNullOrWhiteSpace(cell.ToString()))
            {
                return;
            }
            const string errorMessage = "Currency is duplicated";

            var duplicateOtherCurrency = rawData.FindAll(e => e.Currency == cell.ToString());
                
            var currencyNotContainErrorMessage=duplicateOtherCurrency.FindAll(e =>!e.RemarkErrors.Exists(mes => mes == "Currency is duplicated")) ;
            if (duplicateOtherCurrency.Count <= 0)
            {
                return;
            }
            foreach (var duplicateItem in currencyNotContainErrorMessage)
            {
                duplicateItem.RemarkErrors.Add(errorMessage);
            }
            messageContainer.Add(errorMessage);
        }

        private static bool IsPositiveCell(ICell cell)
        {
            try
            {
                return double.TryParse(cell.ToString(), out var valueB) && valueB >= 0;
            }
            catch
            {
                return false;
            }
        }
        
        public static Result CheckHeaderName(IRow row)
        {
            var cellOne = row.GetCell(0);
            var cellTwo = row.GetCell(1);
            if (cellOne.ToString().ToLower().Trim() != "currency")
            {
                return new Result(1, "Invalid template.");
            }

            if (cellTwo.ToString().ToLower().Trim() != "usdt/unit")
            {
                return new Result(1, "Invalid template.");
            }

            return null;
        }
        
        public static string TruncateBaseRate(string baseRate)
        {
            var words = baseRate.Split('.');
            if (words.Length <= 1) return baseRate;
            var integerPart = words[0];
            var decimalPart = words[1];
            if (decimalPart.Length <= 4) return baseRate;
            var truncatedDecimalPart = decimalPart.Substring(0, 4) + "...";
            return integerPart + "." + truncatedDecimalPart;
        }
        
        public static string FileXMLTemplate(string currency, string usdtValue)
        {
            return $"<File Currency = '{currency}' USDT = '{usdtValue}' />";
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}