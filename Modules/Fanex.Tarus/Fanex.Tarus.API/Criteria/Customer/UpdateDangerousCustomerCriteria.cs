namespace Fanex.Tarus.Entities.Criteria.Customer
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.API.Model.Customer.DangerousCustomer;

    public class UpdateDangerousCustomerCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "CustList")]
        public string ListUserName { get; set; }

        [SpParam(SqlParamName = "DangerType")]
        public byte DangerousType { get; set; }

        [SpParam(SqlParamName = "DangerLevel")]
        public int DangerousLevel { get; set; }

        public string UserName { get; set; }

        public string Ip { get; set; }

        public UpdateDangerousCustomerCriteria(UpdateDangerousCustomerRequest request)
        {
            ListUserName = request.ListUserName;
            DangerousLevel = (int)request.DangerousLevel;
            DangerousType = (byte)request.DangerousType;
            UserName = request.UserName;
            Ip = request.Ip;
        }

        public override string GetSettingKey()
        {
            return "Acc_CustomerUpdDangerCustList";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
