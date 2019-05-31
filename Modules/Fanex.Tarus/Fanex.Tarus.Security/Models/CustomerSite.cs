using System;

namespace Fanex.Tarus.Security.Models
{
    internal sealed class CustomerSite
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

        public bool IsCN88Site
            => IsValid
            && !string.IsNullOrWhiteSpace(Site)
            && Site.Equals("cn88", StringComparison.OrdinalIgnoreCase);
    }
}
