namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig
{
    using Fanex.NAP.Framework.Data;

    public class GetProductSettingByFunctionCriteria : CriteriaBase
    {
        public override string GetSettingKey()
            => "Acc_UMTool_GetProductSettingByFunction";

        public override bool IsValid()
            => true;
    }
}
