namespace Fanex.Tarus.Admin.ManageVendorGlobalSetting.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class SelectVendorGlobalCriteria : CriteriaBase
    {
        public string VendorId { get; set; }

        public string VName { get; set; }

        public string NumberValue { get; set; }

        public string SportType { get; set; }

        public string BetType { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalRows { get; set; }

        public override string GetSettingKey()
            => "Acc_VendorGlobalSetting_Sel";
    }
}
