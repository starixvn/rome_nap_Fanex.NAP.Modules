namespace Fanex.Tarus.Finance.Criteria.Transaction
{
    using Fanex.Tarus.Finance.Entities;

    internal class FinanceCrossTransferBettransInsSiteDBCriteria : FinanceCrossTransferBettransInsCriteria
    {
        public FinanceCrossTransferBettransInsSiteDBCriteria(
            CustomerCrossTransfer param,
            byte siteDBGroupID,
            long? refNo = null) : base(param)
        {
            SiteDBGroupID = siteDBGroupID;
            RefNo = refNo;
        }

        public byte SiteDBGroupID { get; }

        public long? RefNo { get; }

        public override string GetSettingKey()
            => "SiteDB_Acc_FinanceCrossTransferBettransIns";
    }
}
