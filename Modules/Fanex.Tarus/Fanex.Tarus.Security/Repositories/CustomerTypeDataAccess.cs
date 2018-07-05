namespace Fanex.Tarus.Security.DataAccess
{
    using System;
    using Data;
    using NAP.Framework.Data;

    internal sealed class CustomerTypeDataAccess : ICustomerTypeDataAccess
    {
        public bool GetCustomerTypes(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    return reader.Read() && Convert.ToBoolean(reader["isdeposit"]);
                }
            }
        }
    }
}
