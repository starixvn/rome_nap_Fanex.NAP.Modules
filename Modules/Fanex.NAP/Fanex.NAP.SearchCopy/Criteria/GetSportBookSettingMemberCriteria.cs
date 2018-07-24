namespace Fanex.CustomerSetting.Module.SearchCopy.Criteria
{
    using NAP.Framework.Data;

    internal class GetSportBookSettingMemberCriteria : CriteriaBase
    {
        public GetSportBookSettingMemberCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey()
            => "Acc_CustomerSelEditCustomer";

        public override bool IsValid()
            => CustId > 0;
    }
}