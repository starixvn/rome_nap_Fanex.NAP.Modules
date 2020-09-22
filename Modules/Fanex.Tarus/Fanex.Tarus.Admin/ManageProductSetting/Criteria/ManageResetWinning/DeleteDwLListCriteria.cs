namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageResetWinning
{
    using Fanex.NAP.Framework.Data;

    public class DeleteDwLListCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public string Product { get; set; }

        public bool IsTest { get; set; }

        public int UserId { get; set; }

        public string LoginIP { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
            => "Acc_DWLList_Del";

        public override bool IsValid()
            => CustId > 0 && !string.IsNullOrWhiteSpace(Product) && UserId > 0;
    }
}
