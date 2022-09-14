using Fanex.Tarus.Admin.Criteria.SiteManagement;
using Fanex.Tarus.Admin.DataAccess.SiteManagement;
using Fanex.Tarus.Admin.Entities;
using Fanex.Tarus.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Tarus.Admin.Business.SiteManagement
{
    public class SiteManagementService : ISiteManagementService
    {
        private readonly ISiteManagementDataAccess siteManagementDataAccess;

        public SiteManagementService(ISiteManagementDataAccess siteManagementDataAccess = null)
        {
            this.siteManagementDataAccess = siteManagementDataAccess ?? new SiteManagementDataAccess();
        }

        public bool ValidateSite(ValidateSiteCriteria criteria)
        {
            var result = siteManagementDataAccess.ExecuteNonQueryWithResult(criteria);

            return result.ErrorCode == 0;
        }

        public Result AddSite(AddSiteCriteria criteria)
        {
            return siteManagementDataAccess.ExecuteNonQueryWithResult(criteria);
        }

        public List<SelectListItem> GetSiteDBConfig()
        {
            var result = new List<SelectListItem>();
            var listSiteDBConfig = ObjectDbExtensions.GetAllSiteDBConfig();
            foreach (var siteDBConfig in listSiteDBConfig)
            {
                result.Add(new SelectListItem
                {
                    Text = $"{siteDBConfig.SiteDBGroupID} - {siteDBConfig.LinkedServerName}",
                    Value = siteDBConfig.SiteDBGroupID.ToString()
                });
            }

            return result;
        }
    }
}
