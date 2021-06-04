namespace Fanex.Tarus.Admin.ManageVendorGlobalSetting.Models
{
    public class SelectVendorRequest
    {
        public string VendorId { get; set; }

        public string VendorName { get; set; }

        public string NumberValue { get; set; }

        public string SportType { get; set; }

        public string BetType { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
