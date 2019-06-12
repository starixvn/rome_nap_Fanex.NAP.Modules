namespace Fanex.Tarus.Security.Repositories
{
    using System.Collections.Generic;
    using Models;
    using NAP.Framework.Data;

    internal interface ICustomerSiteDataAccess
    {
        IEnumerable<CustomerSite> GetCustomerSites(ICriteria criteria);
    }
}
