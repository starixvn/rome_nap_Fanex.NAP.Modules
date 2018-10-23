namespace Fanex.Tarus.Security.Criteria
{
    using NAP.Framework.Data;

    internal sealed class GetCustomerTypeCriteria : CriteriaBase
    {
        public GetCustomerTypeCriteria(int custId)
            : this(custId.ToString(), null)
        {
        }

        public GetCustomerTypeCriteria(string userNames)
            : this(null, userNames)
        {
        }

        private GetCustomerTypeCriteria(string custIds, string userNames)
        {
            CustIdList = custIds;
            UserNameList = userNames;
        }

        public string CustIdList { get; }

        public string UserNameList { get; }

        public override string GetSettingKey()
            => "Acc_CustSetting_CheckListDeposit";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(CustIdList)
            || !string.IsNullOrWhiteSpace(UserNameList);
    }
}
