namespace Fanex.Tarus.Security.Criteria
{
    using NAP.Framework.Data;

    internal sealed class CheckCustomerTypeCriteria : CriteriaBase
    {
        internal CheckCustomerTypeCriteria(int custId)
            : this(custId, null)
        {
        }

        internal CheckCustomerTypeCriteria(string userName)
            : this(0, userName)
        {
        }

        private CheckCustomerTypeCriteria(int custId, string userName)
        {
            CustId = custId;
            UserName = userName;
        }

        internal int CustId { get; }

        internal string UserName { get; }

        public override string GetSettingKey()
            => "Acc_CustSetting_CheckIsDeposit";

        public override bool IsValid()
            => CustId != 0 || !string.IsNullOrWhiteSpace(UserName);
    }
}
