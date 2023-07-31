namespace Fanex.CustomerSetting.Module.SearchCopy.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class InitBalanceCustomerCommand : CriteriaBase
    {
        public InitBalanceCustomerCommand(int custId, string prefixSPName, bool isCreditBalance)
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
            => $"{PrefixSPName}{(IsCreditBalance ? "Dot_BalanceCreditInitialize" : "Dot_BalanceDebitInitialize")}";

        public override bool IsValid()
            => CustId > 0;
    }
}