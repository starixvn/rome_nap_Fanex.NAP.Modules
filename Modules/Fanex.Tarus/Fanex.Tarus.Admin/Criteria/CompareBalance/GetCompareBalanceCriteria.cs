namespace Fanex.Tarus.Admin.Criteria.CompareBalance
{
    using Fanex.NAP.Framework.Data;

    public class GetCompareBalanceCriteria : CriteriaBase
    {
        public string UserName { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_BalanceMemberSelectAll";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(UserName);
        }
    }
}
