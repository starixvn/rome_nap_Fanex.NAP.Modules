using System;
namespace Fanex.Module.RNGCasino.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class GetIPV6InfoCriteria : CriteriaBase
    {
        public GetIPV6InfoCriteria(string ipList)
            => IPList = ipList;

        public string IPList { get; }

        public override string GetSettingKey()
            => "Age_IPRangesByCountrySelByIPV6List";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(IPList);
    }
}
