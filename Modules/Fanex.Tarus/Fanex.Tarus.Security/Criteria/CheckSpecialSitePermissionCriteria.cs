using System.Web.ModelBinding;
using Sunplus.Accounting.Core.Common;
using Sunplus.Accounting.DataAccess.IBCDLL.Accounting;

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
            UserName = AppSession.AccUserName;
            DeniedList = AccountCredit.Acc_DeniedSiteCurrency_Sel(userId);
        }

        public int UserId { get; }

        public string UserName { get; }

        public int? CustId { get; }

        public string CustName { get; }

        public byte? CheckCustId { get; }

        public string DeniedList { get; }

        public override string GetSettingKey()
            => "Acc_CustomerIsAllowedAccess";

        public override bool IsValid()
            => UserId > 0 && (CustId != null || !string.IsNullOrWhiteSpace(CustName));
    }
}
