namespace Fanex.Tarus.Security.Criteria
{
    using NAP.Framework.Data;

    internal sealed class GetUserPermissionCriteria : CriteriaBase
    {
        internal GetUserPermissionCriteria(int userId)
        {
            CustId = userId;
        }

        internal int CustId { get; }

        public override string GetSettingKey()
            => "Acc_Con_FileNameSelByCustID";

        public override bool IsValid()
            => CustId > 0;
    }
}
