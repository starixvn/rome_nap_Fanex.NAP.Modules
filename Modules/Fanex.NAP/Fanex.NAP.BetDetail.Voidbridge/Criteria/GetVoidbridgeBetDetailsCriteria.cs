namespace Fanex.Voidbridge.BetDetail.Criteria
{
    using System;
    using Fanex.NAP.Framework.Data;
    using Sunplus.Accounting.DataAccess.Report;

    public class GetVoidbridgeBetDetailsCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int TransType { get; set; }

        [SpParam(SqlParamName = "RefNo")]
        public long TransId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRows { get; set; }

        public DateTime MoveBetsDay => ReportUtils.BalanceUp.MoveBetsDay;

        public DateTime BalanceUpDay => ReportUtils.BalanceUp.BalanceUpDay;

        public bool ExcludeBettrans { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_BetList_RNG_Detail";
        }

        public override bool IsValid()
        {
            return TransId > 0;
        }
    }
}
