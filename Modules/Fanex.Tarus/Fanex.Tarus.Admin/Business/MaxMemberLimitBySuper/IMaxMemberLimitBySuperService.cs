namespace Fanex.Tarus.Admin.Business.MaxMemberLimitBySuper
{
    using Criteria.MaxMemberLimitBySuper;
    using Entities.MaxMemberLimitBySuper;
    using Fanex.Tarus.Admin.ViewModel;
    using System.Collections.Generic;

    public interface IMaxMemberLimitBySuperService
    {
        List<SiteViewModel> GetCreditSites(GetSitesCriteria criteria);

        MaxMemberLimitBySuperSearchResult SearchSuper(SearchSuperCriteria criteria);

        List<MaxMemberLimitBySuperUpdateResultEntity> UpdateSuper(UpdateSuperCriteria criteria);
    }
}
