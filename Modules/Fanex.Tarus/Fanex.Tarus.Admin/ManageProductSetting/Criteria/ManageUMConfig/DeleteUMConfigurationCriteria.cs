namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig
{
    using Fanex.NAP.Framework.Data;

    public class DeleteUMConfigurationCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public string IP { get; set; }

        public int ConfigId { get; set; }

        public override string GetSettingKey()
            => "Acc_UMTool_Configuration_Del";

        public override bool IsValid()
            => UserId > 0 && ConfigId > 0;
    }
}
