namespace Fanex.Module.RNGCasino.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class GetIPV4InfoByTopCountryCriteria : CriteriaBase
    {
        public GetIPV4InfoByTopCountryCriteria(string countryCodes)
            => CountryCodes = countryCodes;

        public string CountryCodes { get; }

        public override string GetSettingKey()
            => "Age_IPRangesByCountrySel";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(CountryCodes);
    }

}
