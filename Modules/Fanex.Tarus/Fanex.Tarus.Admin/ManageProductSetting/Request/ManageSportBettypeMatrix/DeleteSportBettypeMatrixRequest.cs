namespace Fanex.Tarus.Admin.ManageProductSetting.Request.ManageSportBettypeMatrix
{
    public class DeleteSportBettypeMatrixRequest
    {
        public int SportType { get; set; }

        public int BetType { get; set; }

        public bool IsLive { get; set; }
    }
}