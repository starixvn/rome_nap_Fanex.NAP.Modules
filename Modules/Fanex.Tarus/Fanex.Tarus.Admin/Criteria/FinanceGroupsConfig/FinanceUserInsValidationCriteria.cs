namespace Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig
{
    using Fanex.NAP.Framework.Data;

    public class FinanceUserInsValidationCriteria : CriteriaBase
    {
        public string UsernameList { get; set; }

        public byte IsAssignUsers { get; set; }

        public string ValidCustIds { get; set; }

        public override string GetSettingKey()
            => "Acc_FinanceGroupsConfig_UsersInsValidation";
    }
}
