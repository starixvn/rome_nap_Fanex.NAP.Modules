namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageResetWinning
{
    using Fanex.NAP.Framework.Data;

    public class SelectDwLListCriteria : CriteriaBase
    {
        public int? CustID { get; set; }

        public string Username { get; set; }

        public int? ProductID { get; set; }

        public string Product { get; set; }

        public bool? IsDeleted { get; set; }

        public bool IsTest { get; set; }

        public override string GetSettingKey()
            => "Acc_DWLList_Sel";

        public override bool IsValid()
            => true;
    }
}
