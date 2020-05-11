namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeMatrix
{
    using Fanex.NAP.Framework.Data;

    public class SelectSportBettypeMatrixCriteria : CriteriaBase
    {
        public string SportType { get; set; }

        public string BetType { get; set; }

        public string ProductId { get; set; }

        public string EventType { get; set; }

        public string RefTable { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public override string GetSettingKey() => "Acc_SportBettypeMatrix_Sel";
    }
}