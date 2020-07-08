namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageRefResource
{
    using Fanex.NAP.Framework.Data;

    public class SelectRefResourceCriteria : CriteriaBase
    {
        public string RefId { get; set; }

        public string ResourceId { get; set; }

        public string ResourceEN { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRows { get; set; }

        public override string GetSettingKey()
            => "Acc_RefResource_Sel";

        public override bool IsValid()
            => true;
    }
}
