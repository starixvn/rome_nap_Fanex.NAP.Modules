namespace Fanex.Tarus.Finance.Criteria.Statement
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Infrastructure.Extensions;

    internal class DailyTxnUpdCheckerCriteria : CriteriaBase
    {
        public DailyTxnUpdCheckerCriteria(
            int checkerId,
            string checkerName,
            string refNos,
            bool isVerified,
            string ip,
            SiteDB siteDB)
        {
            CheckerId = checkerId;
            CheckerName = checkerName;
            RefNos = refNos;
            IsVerified = isVerified;
            IP = ip;
            SiteDB = siteDB;
        }

        public int CheckerId { get; }

        public string CheckerName { get; }

        public string RefNos { get; }

        public bool IsVerified { get; }

        public string IP { get; }

        public byte SiteDBGroupID => (byte)SiteDB.SiteDBGroupID;

        [SpParam(Ignored = true)]
        public SiteDB SiteDB { get; }

        public override string GetSettingKey()
            => SiteDB.IsSiteDB
            ? $"SiteDB{SiteDB.SiteDBGroupID}_Acc_DailyTxn_UpdChecker"
            : "Acc_DailyTxn_UpdChecker";
    }
}
