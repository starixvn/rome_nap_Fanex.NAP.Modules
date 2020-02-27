namespace Fanex.Tarus.Security.Criteria.FinanceAuthorization
{
    using Fanex.NAP.Framework.Data;

    public class CheckFinancePermissionCriteria : CriteriaBase
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public bool CheckCustId { get; set; }

        public override bool IsValid()
        {
            return UserID > 0 && !string.IsNullOrWhiteSpace(UserName);
        }

        public override string GetSettingKey()
        {
            return "Acc_SelFIPermission";
        }
    }
}
