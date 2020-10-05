namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig
{
    public class CheckInUMCriteria : DeleteUMConfigurationCriteria
    {
        public override string GetSettingKey()
            => "Acc_UMTool_InUMWarning_Check";

        public override bool IsValid()
            => ConfigId > 0;
    }
}
