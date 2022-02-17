namespace Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig
{
    using Fanex.NAP.Framework.Data;

    public class FinanceCodeInsValidationCriteria : CriteriaBase
    {
        public string UsernameList { get; set; }

        public byte IsAddCodes { get; set; }

        public override string GetSettingKey()
            => "Acc_FinanceGroupsConfig_CodesInsValidation";
    }
}