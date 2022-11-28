namespace Fanex.Tarus.ExchangeRate.Entities
{
    public class ConfiguringExcel
    {
        public string BaseFileName { get; set; }
        public string BaseHeader { get; set; }
        
        public string FileName
        {
            get => $"{BaseFileName}From{FromDate.Replace("/", "_")}To{ToDate.Replace("/", "_")}";
            set => FileName = value;
        }
        
        public string ObjectDbKey { get; set; }
        
        public string SheetName { get; set; }

        public string Header
        {
            get => $"{BaseHeader} From: {FromDate} - To: {ToDate}";
            set => Header = value;
        }

        public string FromDate { get; set; }
        
        public string ToDate { get; set; }
        
        public string SessionDataKey { get; set; }
        
        public string[] BaseCurrency  { get; set; }
        
        public void Deconstruct(
            out string fdate, 
            out string tdate, 
            out string sessionDataKey,
            out string reportName,
            out string[]  baseCurrency,
            out string  titleCellValue,
            out string baseFileName
            )
        {
            fdate = FromDate;
            tdate = ToDate;
            sessionDataKey = SessionDataKey;
            reportName = FileName;
            baseCurrency = BaseCurrency;
            titleCellValue = Header;
            baseFileName = BaseFileName;
        }
        
        public static ConfiguringExcel OANDAExRateExcel(string fdate, string tdate, string[] baseCurrency)
        {
            return new ConfiguringExcel
            {
                BaseCurrency = baseCurrency,
                BaseHeader = "OANDA Exchange Rate",
                BaseFileName = "OANDAExRate",
                FromDate = fdate,
                ToDate = tdate,
                SessionDataKey = "exchangeRateOanda",
                ObjectDbKey="OANDAExRateSel"
            };
        }
        
        public static ConfiguringExcel CryptoExRateExcel(string fdate, string tdate)
        {
            return new ConfiguringExcel
            {
                BaseCurrency = new [] { "MYR", "CNY", "USDT" },
                BaseHeader = "CoinMarketCap Exchange Rate",
                BaseFileName = "CoinMarketCapExRate",
                FromDate = fdate,
                ToDate = tdate,
                SessionDataKey = "exchangeRateCrypto",
                ObjectDbKey="CryptoExRateSel"
            };
        }
    }
}