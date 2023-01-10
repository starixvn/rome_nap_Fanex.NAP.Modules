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

        public List<SiteViewModel> ListSites { get; set; }

        public string Sites789 => ConfigurationManager.AppSettings["Sites789"];
    }
}
