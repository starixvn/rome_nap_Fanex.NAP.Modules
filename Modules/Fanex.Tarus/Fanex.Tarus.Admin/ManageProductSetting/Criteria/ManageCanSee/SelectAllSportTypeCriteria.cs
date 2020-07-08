namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSporttype
{
    using Fanex.NAP.Framework.Data;

    public class SelectAllSportTypeCriteria : CriteriaBase
    {
        public int Mode => 1;

        public override string GetSettingKey() => "Acc_SporttypeSel";
    }
}