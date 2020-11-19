namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig
{
    using Fanex.NAP.Framework.Data;

    public class GetAllUMConfigurationCriteria : CriteriaBase
    {
        public override string GetSettingKey()
            => "Acc_UMTool_Configuration_Sel";

        public override bool IsValid()
            => true;
    }
}
