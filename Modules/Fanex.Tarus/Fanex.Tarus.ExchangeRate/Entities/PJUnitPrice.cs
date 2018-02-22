namespace Fanex.Tarus.ExchangeRate.Entities
{
    public class PJUnitPrice
    {
        public PJUnitPrice(
            int currencyId,
            string currency,
            string standardCode,
            decimal? unitPrice)
        {
            CurrencyId = currencyId;
            Currency = currency;
            StandardCode = standardCode;
            UnitPrice = unitPrice;
        }

        public int CurrencyId { get; }

        public string Currency { get; }

        public string StandardCode { get; }

        public decimal? UnitPrice { get; }
    }
}