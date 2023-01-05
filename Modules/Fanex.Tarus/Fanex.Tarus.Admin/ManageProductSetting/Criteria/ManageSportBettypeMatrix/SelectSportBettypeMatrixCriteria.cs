namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeMatrix
{
    using Fanex.NAP.Framework.Data;

    public class SelectSportBettypeMatrixCriteria : CriteriaBase
    {
        public int? SportType { get; set; }

        public string SportName { get; set; }

        public int? BetType { get; set; }

        public string BetTypeName { get; set; }

        public string ProductIds { get; set; }

        public int? EventType { get; set; }

        public int? RefTable { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public override string GetSettingKey() => "Acc_SportBettypeMatrix_Sel";
    }
}