namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageRefResource
{

    public class UpdateRefResourceCriteria : InsertRefResourceCriteria
    {
        public bool IsMultiple { get; set; }

        public override string GetSettingKey()
            => "Acc_RefResource_Upd";
    }
}
