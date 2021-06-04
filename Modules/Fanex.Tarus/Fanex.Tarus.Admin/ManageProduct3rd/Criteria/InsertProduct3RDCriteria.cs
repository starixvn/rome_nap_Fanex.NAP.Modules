namespace Fanex.Tarus.Admin.ManageProduct3RD.Criteria
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.ManageProduct3RD.Models;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;

    public class InsertProduct3RDCriteria : CriteriaBase
    {
        public InsertProduct3RDCriteria()
        {
            Entities = new List<Product3RDEntity>();
        }

        [SpParam(Ignored = true)]
        public List<Product3RDEntity> Entities { get; set; }

        public string File
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<ROOT>");
                foreach (var entity in Entities)
                {
                    builder.Append("<File ");
                    builder.Append("ProductId = \"" + entity.ProductId + "\" ");
                    builder.Append("ProductName = \"" + entity.ProductName + "\" ");
                    builder.Append("Description = \"" + entity.Description + "\" ");
                    builder.Append("ProductPrefix = \"" + entity.ProductPrefix + "\" ");
                    builder.Append("OfferSingle = \"" + entity.OfferSingle + "\" ");
                    builder.Append("ApplyDCS = \"" + entity.ApplyDCS + "\" ");
                    builder.Append("Enabled = \"" + entity.Enabled + "\" ");
                    builder.Append("EndOfSupportDate = \"" + entity.EndOfSupportDate + "\" ");
                    builder.Append("OfferSite = \"" + entity.OfferSite + "\" ");
                    builder.Append("/>");
                }

                builder.Append("</ROOT>");
                return builder.ToString();
            }
        }

        public string LoginIP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public override string GetSettingKey() => "Acc_Product3rd_Ins";
    }
}
