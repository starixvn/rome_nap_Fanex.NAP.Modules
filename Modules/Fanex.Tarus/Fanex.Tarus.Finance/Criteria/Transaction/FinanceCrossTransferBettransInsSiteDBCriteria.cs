namespace Fanex.Tarus.Finance.Criteria.Transaction
{
    using Fanex.Tarus.Finance.Entities;
    using Fanex.Tarus.Infrastructure.Extensions;

    internal class FinanceCrossTransferBettransInsSiteDBCriteria : FinanceCrossTransferBettransInsCriteria
    {
        public FinanceCrossTransferBettransInsSiteDBCriteria(
            CustomerCrossTransfer param,
            SiteDB siteDB,
            long? refNo = null) : base(param, siteDB)
            => RefNo = refNo;

        public byte SiteDBGroupID => (byte)SiteDB.SiteDBGroupID;

        public long? RefNo { get; }

        public override string GetSettingKey()
            => SiteDB.IsSiteDB
            ? $"SiteDB{SiteDB.SiteDBGroupID}_SiteDB_Acc_FinanceCrossTransferBettransIns"
            : "SiteDB_Acc_FinanceCrossTransferBettransIns";
    }
}
