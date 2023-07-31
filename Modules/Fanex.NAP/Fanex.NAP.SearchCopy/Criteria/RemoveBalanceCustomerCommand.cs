namespace Fanex.CustomerSetting.Module.SearchCopy.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class RemoveBalanceCustomerCommand : CriteriaBase
    {
        public RemoveBalanceCustomerCommand(int custId, string prefixSPName, bool isCreditBalance)
        {
            CustId = custId;
            IsCreditBalance = isCreditBalance;
            PrefixSPName = prefixSPName;
        }

        public int CustId { get; }

        [SpParam(Ignored = true)]
        private bool IsCreditBalance { get; }

        [SpParam(Ignored = true)]
        private string PrefixSPName { get; }

        public int ErrCode { get; set; }

        public override string GetSettingKey()
            => $"{PrefixSPName}{(IsCreditBalance ? "Dot_BalanceCreditRemove" : "Dot_BalanceDebitRemove")}";

        public override bool IsValid()
            => CustId > 0;
    }
}