namespace Fanex.Tarus.Security.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Data;
    using Models;
    using NAP.Framework.Data;

    internal sealed class CustomerTypeDataAccess : ICustomerTypeDataAccess
    {
        public IEnumerable<CustomerType> GetCustomerTypes(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        yield return ParseCustomerType(reader);
                    }
                }
            }
        }

        private static CustomerType ParseCustomerType(IDataReader reader)
            => new CustomerType(
                reader["custinfo"].ToString(),
                Convert.ToBoolean(reader["isdeposit"]),
                Convert.ToBoolean(reader["isvalid"]),
                Convert.ToBoolean(reader["isinternal"]));
    }
}
