namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageResetWinning
{
    using Fanex.NAP.Framework.Data;

    public class SelectDwLListExtraPTCriteria : CriteriaBase
    {
        public int? CustID { get; set; }

        public string Username { get; set; }

        public int? GroupID { get; set; }

        public string GroupName { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsTest { get; set; }

        public override string GetSettingKey()
            => "Acc_DWLList_ExtraPT_Sel";

        public override bool IsValid()
            => true;
    }
}
