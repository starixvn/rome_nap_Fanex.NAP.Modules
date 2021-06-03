namespace Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.Entities.HeadlineExternalGroupMapping;

    public class BaseExternalGroupHeadlineMappingCriteria : CriteriaBase
    {
        public BaseExternalGroupHeadlineMappingCriteria(HeadlineExternalGroupMappingModel request, int userId)
        {
            GroupHeadlineId = request.GroupHeadlineId;
            SiteId = request.SiteId;
            ExchangeId = request.ExchangeId;
            ExternalGroups = request.ExtraPTGroup;
            PrimaryExtraGroup = request.CurrentPrimaryExtraPTGroup;
            UserId = userId;
        }
        public int GroupHeadlineId { get; set; }

        public int SiteId { get; set; }

        public int ExchangeId { get; set; }

        public string ExternalGroups { get; set; }

        public string PrimaryExtraGroup { get; set; }

        public string IP { get; set; }

        public int UserId { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public string AffectedSupers { get; set; }
    }
}
