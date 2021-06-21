using Fanex.Tarus.Admin.Entities.HeadlineExternalGroupMapping;

namespace Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping
{
    public class DelExternalGroupHeadlineMappingCriteia : BaseExternalGroupHeadlineMappingCriteria
    {
        public DelExternalGroupHeadlineMappingCriteia(HeadlineExternalGroupMappingModel request, int userId) : base(request, userId)
        {
        }

        public string AffectedSupers { get; set; }

        public override string GetSettingKey()
        => "Acc_ExternalGroupHeadline_Del";
    }
}
