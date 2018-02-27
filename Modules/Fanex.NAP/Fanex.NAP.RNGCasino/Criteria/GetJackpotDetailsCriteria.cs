namespace Fanex.Module.RNGCasino.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class GetJackpotDetailsCriteria : CriteriaBase
    {
        public long TransId { get; set; }

        public int CustId { get; set; }

        public int BetType { get; set; }

        public override string GetSettingKey() => "Acc_BetList_Jackpot_Detail";

        public override bool IsValid() => TransId > 0 && CustId > 0 && BetType > 0;
    }
}