namespace Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping
{
    using Fanex.NAP.Framework.Data;

    public class GetHeadlineCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public override string GetSettingKey()
            => "Acc_GroupHeadline_Sel";
    }
}
