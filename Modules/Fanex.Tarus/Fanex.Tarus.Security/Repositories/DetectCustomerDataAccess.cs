using System;
using Fanex.Data;
using Fanex.NAP.Framework.Data;

namespace Fanex.Tarus.Security.Repositories
{
    public interface IDetectCustomerDataAccess
    {
        int GetCustId(ICriteria criteria);
    }

    internal class DetectCustomerDataAccess : IDetectCustomerDataAccess
    {
        public int GetCustId(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                var outputParams = db.GetOuputParams();
                var custId = outputParams["custid"];

                return custId == null || custId == DBNull.Value
                    ? 0
                    : Convert.ToInt32(custId);
            }
        }
    }
}
