namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSporttype
{
    using Fanex.NAP.Framework.Data;

    public class SelectCanSeeCriteria : CriteriaBase
    {

        [SpParam(SqlParamName = "pMutilSportType")]
        public string SportTypes { get; set; }

        public int? BetType { get; set; }

        public string BettypeName { get; set; }

        [SpParam(SqlParamName = "pMutilCurrency")]
        public string Currencies { get; set; }

        [SpParam(SqlParamName = "pMutilSite")]
        public string SiteIDs { get; set; }

        public override string GetSettingKey() => "Acc_ManageCansee_Sel";
    }
}