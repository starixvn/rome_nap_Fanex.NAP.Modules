namespace Fanex.Tarus.Admin.Entities.MaxMemberLimitBySuper
{
    using System.Collections.Generic;

    public class MaxMemberLimitViewModel
    {
        public MaxMemberLimitViewModel()
        {
            ListSites = new List<Site>();
        }

        public List<Site> ListSites { get; set; }
    }
}
