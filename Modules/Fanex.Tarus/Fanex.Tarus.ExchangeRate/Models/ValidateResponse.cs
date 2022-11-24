using System;
using System.Collections.Generic;
using System.Data;
using Sunplus.Accounting.Entities;

namespace Fanex.Tarus.ExchangeRate.Models
{
    public class ValidateResponse
    {
        public DataTable DBValidationResult { get; set; }
        
        public Result Result { get; set; }
        
        public List<string> DuplicatedCurrencyCode { get; set; }
        
        public List<USDTRate> FileData { get; set; }

        public int CompareBillingCycleResult
        {
            get
            {
                var chinaDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "China Standard Time");
                var month= chinaDateTime.Month;
                var year= chinaDateTime.Year;
                var result= TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "China Standard Time")
                    .CompareTo(new DateTime(year, month, 01, 17, 0, 0));
                return result;
            }
        }
    }
}