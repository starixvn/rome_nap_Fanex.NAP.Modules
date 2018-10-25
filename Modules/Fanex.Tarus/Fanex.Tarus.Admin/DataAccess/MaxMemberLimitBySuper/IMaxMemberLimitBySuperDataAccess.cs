namespace Fanex.Tarus.Admin.DataAccess.MaxMemberLimitBySuper
{
    using Criteria.MaxMemberLimitBySuper;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.MaxMemberLimitBySuper;
    using System.Collections.Generic;

    public interface IMaxMemberLimitBySuperDataAccess
    {
        List<Site> GetCreditSites(GetSitesCriteria criteria);

        MaxMemberLimitBySuperSearchResult SearchSuper(SearchSuperCriteria criteria);

        List<MaxMemberLimitBySuperUpdateResultEntity> UpdateSuper(UpdateSuperCriteria criteria);
    }
}
