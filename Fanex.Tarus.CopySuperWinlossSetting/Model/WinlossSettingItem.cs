namespace Fanex.Tarus.CopySuperWinlossSetting.Model
{
    public class WinlossSettingItem
    {
        private const string WinLossLimitXmlFormat = "<Limit SportType=\"{0}\" BetType=\"{1}\" MaxWinLimit=\"{2}\" MaxLossLimit=\"{3}\" IsAutoReset=\"{4}\" />";

        public int SportTypeId { get; set; }

        public int BetTypeId { get; set; }

        public decimal WinLimit { get; set; }

        public decimal MaxWinRange { get; set; }

        public decimal LossLimit { get; set; }

        public decimal MaxLossRange { get; set; }

        public decimal MinLossRange { get; set; }

        public bool IsAutoReset { get; set; }

        public bool IsAutoResetUpline { get; set; }

        public string ToXml()
        {
            return string.Format(WinLossLimitXmlFormat, SportTypeId, BetTypeId, WinLimit, LossLimit, IsAutoReset ? 1 : 0);
        }
    }
}
