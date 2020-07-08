namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageCSSportBettypeMatrix
{
    using Fanex.NAP.Framework.Data;

    public class SelectSportTypeGroupCriteria : CriteriaBase
    {
        public override string GetSettingKey()
            => "Acc_CS_SportGroup_Sel";

        public override bool IsValid()
            => true;
    }
}
