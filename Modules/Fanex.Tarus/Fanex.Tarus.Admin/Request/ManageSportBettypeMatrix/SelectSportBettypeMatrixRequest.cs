namespace Fanex.Tarus.Admin.Request.ManageSportBettypeMatrix
{
    public class SelectSportBettypeMatrixRequest
    {
        public string SportType { get; set; }

        public string BetType { get; set; }

        public string ProductId { get; set; }

        public string EventType { get; set; }

        public string RefTable { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}