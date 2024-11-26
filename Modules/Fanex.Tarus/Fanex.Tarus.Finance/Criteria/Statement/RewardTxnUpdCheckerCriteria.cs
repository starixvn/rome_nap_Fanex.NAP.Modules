namespace Fanex.Tarus.Finance.Criteria.Statement
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Infrastructure.Extensions;

    internal class RewardTxnUpdCheckerCriteria : DailyTxnUpdCheckerCriteria
    {
        public string RefNo { get; set; }

        public RewardTxnUpdCheckerCriteria(
            int checkerId,
            string checkerName,
            string refNos,
            bool isVerified,
            string ip,
            SiteDB siteDB) : base(checkerId, checkerName, refNos, isVerified, ip, siteDB)
        {
            this.RefNo = refNos;
        }

        public override string GetSettingKey()
            => SiteDB.IsSiteDB
            ? $"SiteDB{SiteDB.SiteDBGroupID}_Acc_RewardPayment_UpdChecker"
            : "Acc_RewardPayment_UpdChecker";
    }
}
