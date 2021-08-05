namespace Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping
{
    using Fanex.NAP.Framework.Data;
    public class MappingSuperCriteria : CriteriaBase
    {
        public MappingSuperCriteria(string usernames, int groupId)
        {
            Usernames = usernames;
            GroupId = groupId;
        }

        public string Usernames { get; set; }

        public int GroupId { get; set; }

        public override string GetSettingKey()
            => "Acc_ExternalGroupHeadline_MappingSuper";
    }
}
