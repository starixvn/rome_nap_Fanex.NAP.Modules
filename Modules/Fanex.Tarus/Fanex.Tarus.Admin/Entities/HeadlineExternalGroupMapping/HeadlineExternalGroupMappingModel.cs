namespace Fanex.Tarus.Admin.Entities.HeadlineExternalGroupMapping
{
    public class HeadlineExternalGroupMappingModel
    {
        public int GroupHeadlineId { get; set; }

        public string HeadlineName { get; set; }

        public int SiteId { get; set; }

        public string Site { get; set; }

        public int ExchangeId { get; set; }
        
        public string Currency { get; set; }

        public string NormalGroup { get; set; }

        public string ExtraPTGroup { get; set; }

        public string PrimaryExtraPTGroup { get; set; }

        public string CurrentPrimaryExtraPTGroup { get; set; }

        public string CurrentEffectiveDate { get; set; }
    }
}
