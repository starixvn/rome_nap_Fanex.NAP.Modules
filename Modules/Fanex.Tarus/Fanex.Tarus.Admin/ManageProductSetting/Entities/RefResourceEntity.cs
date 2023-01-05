namespace Fanex.Tarus.Admin.ManageProductSetting.Entities
{
    public class RefResourceEntity
    {
        public string RefId { get; set; }

        public string ResourceId { get; set; }

        public string Sort1 { get; set; }

        public string Sort2 { get; set; }

        public int RowNum { get; set; }

        public string ResourceEN { get; set; }

        public string ResourceCH { get; set; }

        public string ResourceCS { get; set; }

        public string ResourceJP { get; set; }

        public string ResourceIT { get; set; }

        public string ResourceTH { get; set; }

        public string ResourceKO { get; set; }

        public string ResourceES { get; set; }

        public string ResourceEL { get; set; }

        public string ResourceVN { get; set; }

        public string ResourcePL { get; set; }

        public string ResourceID { get; set; }

        public string ResourceHI { get; set; }

        public string ResourceMM { get; set; }

        public string ResourceFR { get; set; }

        public string ResourceRU { get; set; }

        public string ResourcePT { get; set; }

        public string ResourceDE { get; set; }

        public bool IsHaveData =>
            !string.IsNullOrWhiteSpace(RefId) || !string.IsNullOrWhiteSpace(ResourceId) ||
            !string.IsNullOrWhiteSpace(Sort1) || !string.IsNullOrWhiteSpace(Sort2) ||
            !string.IsNullOrWhiteSpace(ResourceEN) || !string.IsNullOrWhiteSpace(ResourceCH) ||
            !string.IsNullOrWhiteSpace(ResourceCS) || !string.IsNullOrWhiteSpace(ResourceJP) ||
            !string.IsNullOrWhiteSpace(ResourceIT) || !string.IsNullOrWhiteSpace(ResourceTH) ||
            !string.IsNullOrWhiteSpace(ResourceKO) || !string.IsNullOrWhiteSpace(ResourceES) ||
            !string.IsNullOrWhiteSpace(ResourceEL) || !string.IsNullOrWhiteSpace(ResourceVN) ||
            !string.IsNullOrWhiteSpace(ResourcePL) || !string.IsNullOrWhiteSpace(ResourceID) ||
            !string.IsNullOrWhiteSpace(ResourceHI) || !string.IsNullOrWhiteSpace(ResourceMM) ||
            !string.IsNullOrWhiteSpace(ResourceFR) || !string.IsNullOrWhiteSpace(ResourceRU) ||
            !string.IsNullOrWhiteSpace(ResourcePT) || !string.IsNullOrWhiteSpace(ResourceDE);
    }
}
