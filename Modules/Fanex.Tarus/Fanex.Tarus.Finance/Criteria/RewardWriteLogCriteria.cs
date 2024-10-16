using Fanex.NAP.Framework.Data;

namespace Fanex.Tarus.Finance.Criteria
{
    public class RewardWriteLogCriteria : CriteriaBase
    {

        public RewardWriteLogCriteria(string paymentLog)
        {
            PaymentLog = paymentLog;
        }

        public string PaymentLog { get; }

        public override bool IsValid() => true;

        public override string GetSettingKey()
            => "WriteLog_UserLog_RunSP_RewardPayment";
    }
}
