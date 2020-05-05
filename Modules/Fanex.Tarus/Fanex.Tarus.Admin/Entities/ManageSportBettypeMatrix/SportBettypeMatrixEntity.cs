namespace Fanex.Tarus.Admin.Entities.ManageSportBettypeMatrix
{
    public class SportBettypeMatrixEntity
    {
        public string SportType { get; set; }

        public string BetType { get; set; }
        public bool IsLive { get; set; }
        public string ProductId { get; set; }
        public bool Enabled { get; set; }
        public bool IsTest { get; set; }
        public string EventType { get; set; }
        public string SubProduct { get; set; }
        public string RefTable { get; set; }
    }
}