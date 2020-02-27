namespace Sunplus.Accounting.Entities.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract]
    public class ExternalSiteExtraPTGroup
    {
        [DataMember]
        public IEnumerable<ExternalSiteExtraPTGroupEntity> Entities { get; set; }

        [DataMember]
        public int NoOfMember { get; set; }
    }

    [Serializable]
    [DataContract]
    public class ExternalSiteExtraPTGroupEntity
    {
        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public DateTime? EffectiveDate { get; set; }

        [DataMember]
        public bool IsPrimary { get; set; }
    }
}
