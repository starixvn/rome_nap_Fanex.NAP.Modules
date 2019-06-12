namespace Fanex.Tarus.Security.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Data;
    using Models;
    using NAP.Framework.Data;

    internal sealed class CustomerSiteDataAccess : ICustomerSiteDataAccess
    {
        public IEnumerable<CustomerSite> GetCustomerSites(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        yield return ParseCustomerSite(reader);
                    }
                }
            }
        }

        private static CustomerSite ParseCustomerSite(IDataReader reader)
            => new CustomerSite(
                reader["custinfo"].ToString(),
                reader["site"].ToString(),
                Convert.ToBoolean(reader["isvalid"]));
    }
}
