namespace Fanex.Tarus.ExchangeRate.Models
{
    using System.Collections.Generic;
    public class USDTRate
    {
        public string CurrencyCode { get; set; }
        public double USDTValue { get; set; } 
        public List<string> RemarkErrors { get; set; } 
    }
}