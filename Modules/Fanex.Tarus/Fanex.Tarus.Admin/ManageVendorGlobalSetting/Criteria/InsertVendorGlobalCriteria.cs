namespace Fanex.Tarus.Admin.ManageVendorGlobalSetting.Criteria
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.ManageVendorGlobalSetting.Models;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;

    public class InsertVendorGlobalCriteria : CriteriaBase
    {
        public InsertVendorGlobalCriteria()
        {
            Entities = new List<VendorGlobalEntity>();
        }

        [SpParam(Ignored = true)]
        public List<VendorGlobalEntity> Entities { get; set; }

        public string File
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<ROOT>");
                foreach (var entity in Entities)
                {
                    builder.Append("<File ");
                    builder.Append("VendorId = \"" + entity.VendorId + "\" ");
                    builder.Append("VendorName = \"" + entity.VendorName + "\" ");
                    builder.Append("NumberValue = \"" + entity.NumberValue + "\" ");
                    builder.Append("SportType = \"" + entity.SportType + "\" ");
                    builder.Append("BetType = \"" + entity.BetType + "\" ");
                    builder.Append("/>");
                }

                builder.Append("</ROOT>");
                return builder.ToString();
            }
        }

        public string LoginIP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public override string GetSettingKey() => "Acc_VendorGlobalSetting_Ins";
    }
}
