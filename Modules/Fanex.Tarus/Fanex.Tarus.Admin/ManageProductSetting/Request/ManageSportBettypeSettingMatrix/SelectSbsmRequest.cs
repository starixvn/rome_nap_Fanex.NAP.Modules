namespace Fanex.Tarus.Admin.ManageProductSetting.Request.ManageSportBettypeSettingMatrix
{
    public class SelectSbsmRequest
    {
        public string SportTypes { get; set; }

        public string BetTypes { get; set; }

        public string ProductId { get; set; }

        public bool IsLicensee { get; set; }

        public bool IsLive { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
