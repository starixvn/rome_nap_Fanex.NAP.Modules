using System.Collections.Generic;
using Fanex.Tarus.Admin.Criteria.SMAManagement;
using Fanex.Tarus.Admin.Entities;
using Fanex.Tarus.Admin.Entities.SMAManagement;

namespace Fanex.Tarus.Admin.DataAccess.SMAManagement
{
    public interface ISMAManagementAccess
    {
        IEnumerable<SiteEntity> GetSites(int mode);

        IEnumerable<IdNameEntity> GetCurrencies(bool excludeTestCurrencies);

        IEnumerable<SMAGroupHeadline> GetSMAGroupHeadlines(GetSMAGroupHeadlineCriteria criteria);

        Result UpdateHeadlineType(UpdateHeadlineTypeCriteria criteria);

        Result UpdateOwner(UpdateOwnerCriteria criteria);
    }
}
