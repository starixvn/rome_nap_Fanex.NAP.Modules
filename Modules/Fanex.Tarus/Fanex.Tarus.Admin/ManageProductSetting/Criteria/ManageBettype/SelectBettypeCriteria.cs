namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageBettype
{
    using Fanex.NAP.Framework.Data;

    public class SelectBettypeCriteria : CriteriaBase
    {
        public int TypeId { get; set; }

        public string TypeNamee { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public override string GetSettingKey() => "Acc_Bettype_Sel";
    }
}