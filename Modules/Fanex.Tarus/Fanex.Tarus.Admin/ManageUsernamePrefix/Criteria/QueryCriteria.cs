namespace Fanex.Tarus.Admin.ManageUsernamePrefix.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class QueryCriteria : CriteriaBase
    {
        public string GroupName { get; set; }

        public string Prefix { get; set; }

        public bool? AllowNewSuper { get; set; }

        public override bool IsValid() => !string.IsNullOrWhiteSpace(GroupName);

        public override string GetSettingKey() => "Acc_SuperPefixByGroupSel";
    }
}
