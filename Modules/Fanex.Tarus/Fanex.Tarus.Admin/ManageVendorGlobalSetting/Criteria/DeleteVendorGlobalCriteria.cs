namespace Fanex.Tarus.Admin.ManageVendorGlobalSetting.Criteria
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;

    public class DeleteVendorGlobalCriteria : CriteriaBase
    {
        public string VendorId { get; set; }

        public string VName { get; set; }

        public string LoginIP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public override string GetSettingKey() => "Acc_VendorGlobalSetting_Del";
    }
}
