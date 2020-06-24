namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSporttype
{
    public class UpdateCanSeeCriteria : InsertCanSeeCriteria
    {
        public int ColumnMode { get; set; }

        public override string GetSettingKey() => "Acc_ManageCansee_Upd";
    }
}