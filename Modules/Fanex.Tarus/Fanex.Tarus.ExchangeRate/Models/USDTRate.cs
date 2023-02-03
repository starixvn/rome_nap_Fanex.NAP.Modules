namespace Fanex.Tarus.ExchangeRate.Models
{
    using System.Collections.Generic;
    public class USDTRate
    {
        public string Currency { get; set; }
        
        public string USDTValue { get; set; } 
        
        public List<string> RemarkErrors { get; set; } 
        
        public string MultipliedBy { get; set; }
        
        public string USDTAfterMultiply { get; set; }
        
        public string ExchangeId { get; set; }
    }
}