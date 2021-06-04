namespace Fanex.Tarus.Admin.ManageProduct3RD.Criteria
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;

    public class DeleteProduct3RDCriteria : CriteriaBase
    {
        public string ProductId { get; set; }

        public string LoginIP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;
        
        public override string GetSettingKey()
            => "Acc_Product3rd_Del";
    }
}
