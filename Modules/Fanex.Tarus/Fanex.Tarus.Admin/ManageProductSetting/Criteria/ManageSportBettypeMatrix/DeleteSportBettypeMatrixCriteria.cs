namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeMatrix
{
    using Fanex.NAP.Framework.Data;

    public class DeleteSportBettypeMatrixCriteria : CriteriaBase
    {
        public int SportType { get; set; }

        public int BetType { get; set; }

        public bool IsLive { get; set; }

        public int UserId { get; set; }

        public string LoginIp { get; set; }

        public override string GetSettingKey() => "Acc_SportBettypeMatrix_Del";
    }
}