namespace Fanex.Tarus.Finance.Criteria
{
    using Fanex.Tarus.Infrastructure.Extensions;

    internal class ReverseTransferRollbackSiteDBCriteria : TransferRollbackSiteDBCriteria
    {
        public ReverseTransferRollbackSiteDBCriteria(long refNo, int userId, string ip, SiteDB siteDB)
            : base(refNo, userId, ip, siteDB)
        {
        }

        public override string GetSettingKey()
            => SiteDB.IsSiteDB
            ? $"SiteDB{SiteDBGroupID}_SiteDB_Acc_ReverseTransfer_Rollback"
            : "SiteDB_Acc_ReverseTransfer_Rollback";
    }
}
