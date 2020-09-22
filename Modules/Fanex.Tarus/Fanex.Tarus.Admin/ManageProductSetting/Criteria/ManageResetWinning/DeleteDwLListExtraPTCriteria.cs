using Fanex.NAP.Framework.Data;

namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageResetWinning
{
    public class DeleteDwLListExtraPTCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public bool IsTest { get; set; }

        public int UserId { get; set; }

        public string LoginIP { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
            => "Acc_DWLList_ExtraPT_Del";

        public override bool IsValid()
            => CustId > 0 && UserId > 0;
    }
}
