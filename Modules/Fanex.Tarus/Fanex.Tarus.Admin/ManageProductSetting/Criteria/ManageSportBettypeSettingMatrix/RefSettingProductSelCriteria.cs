namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeSettingMatrix
{
    using Fanex.NAP.Framework.Data;

    public class RefSettingProductSelCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "pMutilProductID")]
        public string ProductIds { get; set; }

        public override string GetSettingKey()
            => "Acc_ReferenceSetting_ProductSel";
    }
}
