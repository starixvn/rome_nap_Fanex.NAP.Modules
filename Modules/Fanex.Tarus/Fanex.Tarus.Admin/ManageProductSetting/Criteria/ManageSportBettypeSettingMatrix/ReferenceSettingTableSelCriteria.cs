namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeSettingMatrix
{
    using Fanex.NAP.Framework.Data;

    public class ReferenceSettingTableSelCriteria : CriteriaBase
    {
        public int TableType { get; set; }

        public override string GetSettingKey()
         => "Acc_ReferenceSetting_TableSel";
    }
}
