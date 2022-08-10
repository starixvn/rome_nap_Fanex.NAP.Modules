namespace Fanex.Module.RNGCasino.Criteria
{
    using System;
    using Fanex.NAP.Framework.Data;
    using Sunplus.Accounting.DataAccess.Report;

    public class GetJackpotDetailsCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public string Title { get; set; }

        public long TransId { get; set; }

        public int CustId { get; set; }

        public int BetType { get; set; }

        public DateTime MoveBetsDay => ReportUtils.BalanceUp.MoveBetsDay;

        public DateTime BalanceUpDay => ReportUtils.BalanceUp.BalanceUpDay;

        public bool ExcludeBettrans { get; set; }

        public override string GetSettingKey() => "Acc_BetList_Jackpot_Detail";

        public override bool IsValid() => TransId > 0 && CustId > 0 && BetType > 0;
    }
}