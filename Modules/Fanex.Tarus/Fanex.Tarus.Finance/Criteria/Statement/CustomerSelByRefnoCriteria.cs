namespace Fanex.Tarus.Finance.Criteria.Statement
{
    using Fanex.NAP.Framework.Data;

    internal class CustomerSelByRefnoCriteria : CriteriaBase
    {
        public CustomerSelByRefnoCriteria(long refNo, bool isReward)
        {
            RefNo = refNo;
            IsReward = isReward;
        }

        public long RefNo { get; }

        public bool IsReward { get; }

        public override string GetSettingKey()
            => IsReward ? "Acc_RewardPayment_CustomerSelByRefNo" : "Acc_CustomerSelByRefno";
    }
}
