namespace Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig
{
    using Fanex.NAP.Framework.Data;

    public class FinanceGroupInsValidationCriteria : CriteriaBase
    {
        public string FinanceUser { get; set; }

        public string FinanceCode { get; set; }

        public byte IsCreateGroup { get; set; }

        public string ValidCustIds { get; set; }

        public override string GetSettingKey()
            => "Acc_FinanceGroupsConfig_FinanceGroupInsValidation";
    }
}
