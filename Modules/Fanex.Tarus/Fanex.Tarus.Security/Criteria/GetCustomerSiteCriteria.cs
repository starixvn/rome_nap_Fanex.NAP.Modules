namespace Fanex.Tarus.Security.Criteria
{
    using NAP.Framework.Data;

    internal sealed class GetCustomerSiteCriteria: CriteriaBase
    {
        public GetCustomerSiteCriteria(int custId)
            : this(custId.ToString(), null)
        {
        }

        public GetCustomerSiteCriteria(string userNames)
            : this(null, userNames)
        {
        }

        private GetCustomerSiteCriteria(string custIds, string userNames)
        {
            CustIdList = custIds;
            UserNameList = userNames;
        }

        public string CustIdList { get; }

        public string UserNameList { get; }

        public override string GetSettingKey()
            => "Acc_CustSetting_ListSiteSel";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(CustIdList)
            || !string.IsNullOrWhiteSpace(UserNameList);
    }
}
