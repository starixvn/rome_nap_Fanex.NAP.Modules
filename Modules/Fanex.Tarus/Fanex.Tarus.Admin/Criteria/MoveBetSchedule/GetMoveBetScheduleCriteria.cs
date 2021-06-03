namespace Fanex.Tarus.Admin.Criteria.MoveBetSchedule
{
    using Fanex.NAP.Framework.Data;
    using System;

    public class GetMoveBetScheduleCriteriaBase : CriteriaBase
    {
        public virtual bool IsDefault { get; }

        public override string GetSettingKey() => "Acc_Manual_MoveBet_Sel";
    }

    public class GetDefaultMoveBetScheduleCriteria : GetMoveBetScheduleCriteriaBase
    {
        public override bool IsDefault => true;

        public override bool IsValid() => true;
    }

    public class GetMoveBetScheduleCriteria : GetMoveBetScheduleCriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public override bool IsDefault => false;

        public override bool IsValid() => FromDate <= ToDate;
    }
}