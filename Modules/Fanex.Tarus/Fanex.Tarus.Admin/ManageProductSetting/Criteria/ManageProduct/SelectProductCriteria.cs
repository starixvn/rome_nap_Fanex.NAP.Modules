namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageProduct
{
    using Fanex.NAP.Framework.Data;

    public class SelectProductCriteria : CriteriaBase
    {
        public int? ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductPrefix { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalRows { get; set; }

        public override string GetSettingKey() => "Acc_Product_Sel";
    }
}