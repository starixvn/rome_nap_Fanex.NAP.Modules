namespace Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig
{
    using Fanex.NAP.Framework.Data;

    public class FinanceGroupGetValidCustIdCriteria : CriteriaBase
    {
        public string ValidCustIds { get; set; }

        public override string GetSettingKey()
            => "Acc_FinanceGroupsConfig_GetValidCustid";
    }
}
