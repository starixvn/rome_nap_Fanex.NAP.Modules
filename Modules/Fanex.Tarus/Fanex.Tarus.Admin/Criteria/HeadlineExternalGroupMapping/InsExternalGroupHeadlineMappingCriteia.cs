using Fanex.Tarus.Admin.Entities.HeadlineExternalGroupMapping;

namespace Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping
{
    public class InsExternalGroupHeadlineMappingCriteia : BaseExternalGroupHeadlineMappingCriteria
    {
        public InsExternalGroupHeadlineMappingCriteia(HeadlineExternalGroupMappingModel request, int userId) : base(request, userId)
        {
            EffectiveDate = request.CurrentEffectiveDate;
        }

        public string EffectiveDate { get; set; }

        public override string GetSettingKey()
            => "Acc_ExternalGroupHeadline_Ins";
    }
}
