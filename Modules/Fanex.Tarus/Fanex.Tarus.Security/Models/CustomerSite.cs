namespace Fanex.Tarus.Security.Models
{
    public sealed class CustomerSite
    {
        public CustomerSite(string custInfo, string site, bool isValid)
        {
            CustInfo = custInfo;
            Site = site;
            IsValid = isValid;
        }

        public string CustInfo { get; }

        public string Site { get; }

        public bool IsValid { get; }
    }
}
