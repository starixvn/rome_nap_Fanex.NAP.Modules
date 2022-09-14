namespace Fanex.Tarus.Finance.Criteria
{
    using Fanex.NAP.Framework.Data;

    internal class GetSiteDBConfigByRefNoCriteria : CriteriaBase
    {
        public GetSiteDBConfigByRefNoCriteria(string refNoList)
            => RefNos = refNoList;

        public string RefNos { get; }

        public override string GetSettingKey()
            => "Acc_SiteDBGroupIDSelByRefnos";
    }
}
