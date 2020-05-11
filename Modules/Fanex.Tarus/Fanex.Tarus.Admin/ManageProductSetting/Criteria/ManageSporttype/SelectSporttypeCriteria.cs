namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSporttype
{
    using Fanex.NAP.Framework.Data;

    public class SelectSportTypeCriteria : CriteriaBase
    {
        public int SportType { get; set; }

        public string SportName { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public override string GetSettingKey() => "Acc_Sporttype_Sel";
    }
}