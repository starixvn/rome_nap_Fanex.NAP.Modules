namespace Sunplus.Accounting.Entities
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class IpRange
    {
        public IEnumerable<IPVNBean> Ipv4List { get; set; }

        public IEnumerable<Ipv6Range> Ipv6List { get; set; }
    }
}