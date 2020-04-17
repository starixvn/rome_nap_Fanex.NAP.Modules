namespace Fanex.Tarus.Admin.Request.ManageSportType
{
    public class SelectRequest
    {
        public int SportType { get; set; }

        public string SportName { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
