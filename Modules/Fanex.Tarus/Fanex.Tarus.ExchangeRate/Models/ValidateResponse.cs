namespace Fanex.Tarus.ExchangeRate.Models
{
    using System;
    public class ValidateResponse :ValidateResult
    {
        // FE use CompareBillingCycleResult
        public int CompareBillingCycleResult
        {
            get
            {
                var chinaDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "China Standard Time");
                var month= chinaDateTime.Month;
                var year= chinaDateTime.Year;
                var result= TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "China Standard Time")
                    .CompareTo(new DateTime(year, month, 05, 17, 0, 0));
                return result;
            }
        }
    }
}