using Fanex.NAP.Framework.Data;

namespace Fanex.Tarus.Finance.Criteria
{
    public class RewardWriteLogCriteria : CriteriaBase
    {
        public string PaymentLog { get; set; }

        public override bool IsValid() => true;

        public override string GetSettingKey()
            => "WriteLog_UserLog_RunSP_RewardPayment";
    }
}
