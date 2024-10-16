using Fanex.Data;
using Fanex.Tarus.Finance.Criteria;

namespace Fanex.Tarus.Finance.DataAccess
{
    public class BaseDataAccess
    {
        public void RewardWriteLog(RewardWriteLogCriteria criteria)
        {
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.ExecuteNonQuery(criteria.GetSpParams());
            }
        }
    }
}
