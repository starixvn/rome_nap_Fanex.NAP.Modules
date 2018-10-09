namespace Fanex.Tarus.Admin.Business.MaxMemberLimitBySuper
{
    using Criteria.MaxMemberLimitBySuper;
    using Entities;
    using Entities.MaxMemberLimitBySuper;
    using System.Collections.Generic;

    public interface IMaxMemberLimitBySuperService
    {
        List<Site> GetCreditSites(GetSitesCriteria criteria);

        MaxMemberLimitBySuperSearchResult SearchSuper(SearchSuperCriteria criteria);

        List<MaxMemberLimitBySuperUpdateResultEntity> UpdateSuper(UpdateSuperCriteria criteria);
    }
}
