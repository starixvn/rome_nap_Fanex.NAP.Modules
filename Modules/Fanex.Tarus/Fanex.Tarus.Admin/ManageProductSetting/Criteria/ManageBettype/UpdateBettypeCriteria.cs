namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageBettype
{
    public class UpdateBettypeCriteria : InsertBettypeCriteria
    {
        public bool IsMultiple { get; set; }
        public override string GetSettingKey() => "Acc_Bettype_Upd";
    }
}