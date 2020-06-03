namespace Fanex.Tarus.Admin.ManageProductSetting.Request.ManageProduct
{
    public class SelectProductRequest
    {
        public int? ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductPrefix { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}