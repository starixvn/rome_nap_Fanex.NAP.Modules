using Fanex.NAP.Framework.Data;

namespace Fanex.Tarus.Admin.Criteria.ManageProduct
{
    public class SelectProductCriteria : CriteriaBase
    {
        public string ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductPrefix { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public override string GetSettingKey() => "Acc_Product_Sel";
    }
}