using Fanex.Tarus.Finance.Criteria;
using Fanex.Tarus.Finance.DataAccess;

namespace Fanex.Tarus.Finance.Business
{
    public class RewardBaseService
    {
        protected BaseDataAccess _baseDataAccess;

        public RewardBaseService() : this(null)
        {
        }

        public RewardBaseService(BaseDataAccess baseDataAccess)
        {
            _baseDataAccess = baseDataAccess ?? new BaseDataAccess();
        }

        public void RewardWriteLog(string logData)
            => this._baseDataAccess.RewardWriteLog(new RewardWriteLogCriteria(logData));
    }
}
