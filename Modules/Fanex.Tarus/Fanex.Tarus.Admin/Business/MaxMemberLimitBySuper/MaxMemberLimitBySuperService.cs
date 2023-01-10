using Fanex.Tarus.Admin.Criteria.MaxMemberLimitBySuper;
using Fanex.Tarus.Admin.DataAccess.MaxMemberLimitBySuper;
using Fanex.Tarus.Admin.Entities.MaxMemberLimitBySuper;
using Fanex.Tarus.Admin.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Tarus.Admin.Business.MaxMemberLimitBySuper
{
    public class MaxMemberLimitBySuperService : IMaxMemberLimitBySuperService
    {
        private readonly MaxMemberLimitBySuperDataAccess _maxMemberLimitBySuperDataAccess;

        public MaxMemberLimitBySuperService()
            : this(null)
        { }

        public MaxMemberLimitBySuperService(MaxMemberLimitBySuperDataAccess maxMemberLimitBySuperDataAccess)
        {
            _maxMemberLimitBySuperDataAccess = maxMemberLimitBySuperDataAccess ?? new MaxMemberLimitBySuperDataAccess();
        }

        public List<SiteViewModel> GetCreditSites(GetSitesCriteria criteria)
        {
            var sites = new List<SiteViewModel>();
            _maxMemberLimitBySuperDataAccess.GetCreditSites(criteria)
               .GroupBy(p => p.SiteLabel)
               .ToList()
               .ForEach(grp =>
               {
                   if (string.IsNullOrEmpty(grp.Key))
                   {
                       sites.AddRange(grp.Select(pt => new SiteViewModel(pt.SiteId.ToString(), pt.SiteName, null)));
                   }
                   else
                   {
                       sites.Add(new SiteViewModel(grp.Key, grp.Key, grp.Select(p => new SiteViewModel(p.SiteId.ToString(), p.SiteName, null))));
                   }
               });

            return sites;
        }   

        public MaxMemberLimitBySuperSearchResult SearchSuper(SearchSuperCriteria criteria) => _maxMemberLimitBySuperDataAccess.SearchSuper(criteria);

        public List<MaxMemberLimitBySuperUpdateResultEntity> UpdateSuper(UpdateSuperCriteria criteria) => _maxMemberLimitBySuperDataAccess.UpdateSuper(criteria);
    }
}
