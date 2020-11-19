namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig
{
    using Fanex.NAP.Framework.Data;

    public class DeleteOldDateCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public override string GetSettingKey()
            => "Acc_UMTool_Configuration_Del_OldData";

        public override bool IsValid()
            => UserId > 0;
    }
}
