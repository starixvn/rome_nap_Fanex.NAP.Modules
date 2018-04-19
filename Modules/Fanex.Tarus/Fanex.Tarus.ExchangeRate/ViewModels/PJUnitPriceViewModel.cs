namespace Fanex.Tarus.ExchangeRate.ViewModels
{
    using System.Collections.Generic;
    using Entities;

    public class PJUnitPriceViewModel
    {
        public PJUnitPriceViewModel(IEnumerable<PJUnitPrice> pjUnitPrices, bool hasHostPermission)
        {
            PJUnitPrices = pjUnitPrices;
            HasHostPermission = hasHostPermission;
        }
        public IEnumerable<PJUnitPrice> PJUnitPrices { get; }

        public bool HasHostPermission { get; }
    }
}
