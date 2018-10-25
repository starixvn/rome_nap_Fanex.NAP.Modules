using Fanex.Tarus.Admin.Criteria.MaxMemberLimitBySuper;
using Fanex.Tarus.Admin.DataAccess.MaxMemberLimitBySuper;
using Fanex.Tarus.Admin.Entities;
using Fanex.Tarus.Admin.Entities.MaxMemberLimitBySuper;
using Sunplus.Accounting.Core.Helper;
using System.Collections.Generic;
using System.Web;

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

        public List<Site> GetCreditSites(GetSitesCriteria criteria) => _maxMemberLimitBySuperDataAccess.GetCreditSites(criteria);

        public MaxMemberLimitBySuperSearchResult SearchSuper(SearchSuperCriteria criteria) => _maxMemberLimitBySuperDataAccess.SearchSuper(criteria);

        public List<MaxMemberLimitBySuperUpdateResultEntity> UpdateSuper(UpdateSuperCriteria criteria) => _maxMemberLimitBySuperDataAccess.UpdateSuper(criteria);
    }
}
