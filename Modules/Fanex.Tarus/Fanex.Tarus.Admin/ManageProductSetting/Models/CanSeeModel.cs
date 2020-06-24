namespace Fanex.Tarus.Admin.ManageProductSetting.Models
{
    public class CanSeeModel
    {
        public int SportType { get; set; }

        public string SportTypeName { get; set; }

        public int BetType { get; set; }

        public string BetTypeName { get; set; }

        public int Currency { get; set; }

        public string CurrencyName { get; set; }

        public int SiteId { get; set; }

        public string SiteName { get; set; }

        public bool IsLaunched { get; set; }

        public string IsLaunchedText => IsLaunched ? "Offer" : "Not Offer";

        public bool IsTest { get; set; }

        public int LaunchedLevel { get; set; }
    }
}