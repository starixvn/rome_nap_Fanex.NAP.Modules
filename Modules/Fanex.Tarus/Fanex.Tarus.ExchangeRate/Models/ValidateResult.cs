namespace Fanex.Tarus.ExchangeRate.Models
{
    using System.Collections.Generic;
    using Sunplus.Accounting.Entities;
    
    public abstract class ValidateResult
    {
        public IEnumerable<USDTRate> Items { get; set; }
        
        public Result Result { get; set; }
    }
}