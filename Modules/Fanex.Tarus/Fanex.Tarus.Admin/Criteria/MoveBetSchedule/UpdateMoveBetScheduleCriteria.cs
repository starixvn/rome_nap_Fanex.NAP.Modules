namespace Fanex.Tarus.Admin.Criteria.MoveBetSchedule
{
    using Fanex.NAP.Framework.Data;
    using Sunplus.Accounting.Core.Common;
    using Sunplus.Accounting.Core.Helper;
    using System;

    public class UpdateMoveBetScheduleCriteria : CriteriaBase
    {
        public int UserID => AppSession.AccId;

        public string LoginIP => Utils.GetClientIP();

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string File { get; set; }

        public override string GetSettingKey() => "Acc_Manual_MoveBet_Adjust";

        public override bool IsValid() => UserID >= 0 && FromDate <= ToDate;
    }
}