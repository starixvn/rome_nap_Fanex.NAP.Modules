namespace Fanex.Tarus.Security.Criteria
{
    using NAP.Framework.Data;

    internal sealed class CheckSpecialSitePermissionCriteria : CriteriaBase
    {
        public CheckSpecialSitePermissionCriteria(int userId, int? custId, string custName)
            : this(userId, custId, custName, null)
        {
        }

        private CheckSpecialSitePermissionCriteria(
            int userId,
            int? custId,
            string custName,
            byte? checkCustId)
        {
            UserId = userId;
            CustId = custId;
            CustName = custName;
            CheckCustId = checkCustId;
        }

        public int UserId { get; }

        public int? CustId { get; }

        public string CustName { get; }

        public byte? CheckCustId { get; }

        public override string GetSettingKey()
            => "Acc_CustomerIsAllowedAccess";

        public override bool IsValid()
            => UserId > 0 && (CustId != null || !string.IsNullOrWhiteSpace(CustName));
    }
}
