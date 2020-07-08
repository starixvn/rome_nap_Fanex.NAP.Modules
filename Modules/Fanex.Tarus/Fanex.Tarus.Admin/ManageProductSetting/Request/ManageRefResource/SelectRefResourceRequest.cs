namespace Fanex.Tarus.Admin.ManageProductSetting.Request.ManageRefResource
{
    public class SelectRefResourceRequest
    {
        public string RefId { get; set; }

        public string ResourceId { get; set; }

        public string ResourceEN { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
