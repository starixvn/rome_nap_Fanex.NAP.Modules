namespace Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig
{
    using Fanex.NAP.Framework.Data;

    public class FinanceGroupSelCriteria : CriteriaBase
    {
        public string GroupIds { get; set; }

        public string UsernameList { get; set; }

        public int FinanceType { get; set; }

        public int? Level { get; set; }

        public int? IsView { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public override string GetSettingKey()
            => "Acc_FinanceGroupsConfig_FinanceGroupSel";
    }
}
