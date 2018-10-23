namespace Fanex.Tarus.Security.Criteria
{
    using NAP.Framework.Data;

    internal sealed class GetUserPermissionCriteria : CriteriaBase
    {
        public GetUserPermissionCriteria(int userId)
        {
            CustId = userId;
        }

        public int CustId { get; }

        public override string GetSettingKey()
            => "Acc_Con_FileNameSelByCustID";

        public override bool IsValid()
            => CustId > 0;
    }
}
