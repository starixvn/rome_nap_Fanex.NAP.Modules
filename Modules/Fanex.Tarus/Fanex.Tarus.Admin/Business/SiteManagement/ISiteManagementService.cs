using Fanex.Tarus.Admin.Criteria.SiteManagement;
using Fanex.Tarus.Admin.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Tarus.Admin.Business.SiteManagement
{
    public interface ISiteManagementService
    {
        bool ValidateSite(ValidateSiteCriteria criteria);

        Result AddSite(AddSiteCriteria criteria);

        List<SelectListItem> GetSiteDBConfig();
    }
}
