namespace Fanex.Tarus.Admin.ManageProductSetting.Request.ManageSportType
{
    public class SelectSportRequest
    {
        public int? SportType { get; set; }

        public string SportName { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}