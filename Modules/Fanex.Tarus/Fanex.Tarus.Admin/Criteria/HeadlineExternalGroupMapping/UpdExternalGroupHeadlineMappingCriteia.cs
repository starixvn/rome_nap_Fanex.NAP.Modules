using Fanex.Tarus.Admin.Entities.HeadlineExternalGroupMapping;

namespace Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping
{
    public class UpdExternalGroupHeadlineMappingCriteia : InsExternalGroupHeadlineMappingCriteia
    {
        public UpdExternalGroupHeadlineMappingCriteia(HeadlineExternalGroupMappingModel request, int userId) : base(request, userId)
        {
        }

        public override string GetSettingKey()
            => "Acc_ExternalGroupHeadline_Upd";
    }
}
