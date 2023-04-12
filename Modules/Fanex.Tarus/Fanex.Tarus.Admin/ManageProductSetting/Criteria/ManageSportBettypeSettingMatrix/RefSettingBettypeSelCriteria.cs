namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeSettingMatrix
{
    using Fanex.NAP.Framework.Data;

    public class RefSettingBettypeSelCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "pMutilProductID")]
        public string ProductIds { get; set; }

        [SpParam(SqlParamName = "pMutilSportType")]
        public string SportTypes { get; set; }

        [SpParam(SqlParamName = "pMutilBetType")]
        public string BetTypes { get; set; }

        public override string GetSettingKey()
            => "Acc_ReferenceSetting_BetTypeSel";
    }
}
