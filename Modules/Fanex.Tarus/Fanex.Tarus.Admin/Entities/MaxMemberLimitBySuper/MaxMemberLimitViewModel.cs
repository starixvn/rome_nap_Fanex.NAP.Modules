namespace Fanex.Tarus.Admin.Entities.MaxMemberLimitBySuper
{
    using Fanex.Tarus.Admin.ViewModel;
    using System.Collections.Generic;
    using System.Configuration;

    public class MaxMemberLimitViewModel
    {
        public MaxMemberLimitViewModel()
        {
            ListSites = new List<SiteViewModel>();
        }

        public IEnumerable<SiteViewModel> ListSites { get; set; }
    }
}
