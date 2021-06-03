namespace Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping
{
    using Fanex.NAP.Framework.Data;

    public class SelExternalGroupHeadlineMappingCriteia : CriteriaBase
    {
        public string HeadlineName { get; set; }

        public string SiteIds { get; set; }

        public string ExchangeIds { get; set; }

        public string ExternalGroups { get; set; }

        public string PrimaryExtraGroups { get; set; }

        public override string GetSettingKey()
            => "Acc_ExternalGroupHeadline_Sel";
    }
}
