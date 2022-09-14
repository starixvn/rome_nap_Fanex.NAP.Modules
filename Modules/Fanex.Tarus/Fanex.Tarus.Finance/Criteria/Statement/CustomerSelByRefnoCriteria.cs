namespace Fanex.Tarus.Finance.Criteria.Statement
{
    using Fanex.NAP.Framework.Data;

    internal class CustomerSelByRefnoCriteria : CriteriaBase
    {
        public CustomerSelByRefnoCriteria(long refNo)
            => RefNo = refNo;

        public long RefNo { get; }

        public override string GetSettingKey()
            => "Acc_CustomerSelByRefno";
    }
}
