namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSporttype
{
    using Fanex.NAP.Framework.Data;

    public class SelectCanSeeCriteria : CriteriaBase
    {
        public int SportType { get; set; }

        public int? BetType { get; set; }

        public string BettypeName { get; set; }

        [SpParam(SqlParamName = "pMutilCurrency")]
        public string Currencies { get; set; }

        [SpParam(SqlParamName = "pMutilSite")]
        public string SiteIds { get; set; }

        public override string GetSettingKey() => "Acc_ManageCansee_Sel";
    }
}