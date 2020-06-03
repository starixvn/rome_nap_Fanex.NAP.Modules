namespace Fanex.Tarus.Admin.ManageProductSetting.Request.ManageSportBettypeMatrix
{
    public class SelectSportBettypeMatrixRequest
    {
        public int? SportType { get; set; }

        public string SportName { get; set; }

        public int? BetType { get; set; }

        public string BetTypeName { get; set; }

        public string ProductId { get; set; }

        public int? EventType { get; set; }

        public int? RefTable { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}