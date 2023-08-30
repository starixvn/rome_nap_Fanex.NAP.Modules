namespace Fanex.Module.RNGCasino.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class GetIPV4InfoCriteria : CriteriaBase
    {
        public GetIPV4InfoCriteria(string ipList)
            => IPList = ipList;

        public string IPList { get; }

        public override string GetSettingKey()
            => "Age_IPRangesByCountrySelByIPList";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(IPList);
    }
}
