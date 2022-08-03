namespace Fanex.Tarus.Finance.Criteria
{
    internal class BetTransInsertTransferSiteDBCriteria : BetTransInsertTransferCriteria
    {
        internal BetTransInsertTransferSiteDBCriteria(
            int fromCustId,
            string toUsername,
            decimal amount,
            double exchangeRate,
            string remark,
            int userId,
            string ip,
            bool isCollection,
            byte siteDbGroupId,
            long? refNo = null) : base(fromCustId, toUsername, amount, exchangeRate, remark, userId, ip, isCollection)
        {
            SiteDbGroupId = siteDbGroupId;
            RefNo = refNo;
        }

        public byte SiteDbGroupId { get; }

        public long? RefNo { get; }

        public override string GetSettingKey()
            => "SiteDB_Acc_BettransInsTransfer";
    }
}
