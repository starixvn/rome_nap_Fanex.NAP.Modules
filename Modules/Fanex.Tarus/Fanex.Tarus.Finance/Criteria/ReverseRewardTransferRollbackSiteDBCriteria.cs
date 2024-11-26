namespace Fanex.Tarus.Finance.Criteria
{
    using Fanex.Tarus.Infrastructure.Extensions;

    internal class ReverseRewardTransferRollbackSiteDBCriteria : TransferRollbackSiteDBCriteria
    {
        public int Creator { get; set; }

        public ReverseRewardTransferRollbackSiteDBCriteria(long refNo, int userId, string ip, SiteDB siteDB)
            : base(refNo, userId, ip, siteDB)
        {
            Creator = userId;
        }

        public override string GetSettingKey()
            => SiteDB.IsSiteDB
            ? $"SiteDB{SiteDBGroupID}_Acc_RewardPayment_Reverse_Rollback"
            : "Acc_RewardPayment_Reverse_Rollback";
    }
}
