namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig
{
    using Fanex.NAP.Framework.Data;

    public class UpdUMConfigurationCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public string IP { get; set; }

        public int ConfigID { get; set; }

        public string BeginTime { get; set; }

        public string EndTime { get; set; }

        public string ShowTime { get; set; }

        public bool LockAddingNew { get; set; }

        public override string GetSettingKey()
            => "Acc_UMTool_Configuration_Upd";

        public override bool IsValid()
            => UserId > 0
            && ConfigID > 0
            && !string.IsNullOrWhiteSpace(BeginTime)
            && !string.IsNullOrWhiteSpace(EndTime)
            && !string.IsNullOrWhiteSpace(ShowTime);
    }
}
