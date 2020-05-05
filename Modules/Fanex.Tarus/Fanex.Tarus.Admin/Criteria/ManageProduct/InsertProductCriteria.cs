using System.Collections.Generic;
using System.Text;
using Fanex.NAP.Framework.Data;
using Fanex.Tarus.Admin.Entities.ManageProduct;

namespace Fanex.Tarus.Admin.Criteria.ManageProduct
{
    public class InsertProductCriteria : CriteriaBase
    {
        public InsertProductCriteria()
        {
            Entities = new List<ProductEntity>();
        }

        [SpParam(Ignored = true)]
        public List<ProductEntity> Entities { get; set; }

        public string File
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<ROOT>");
                foreach (var entity in Entities)
                {
                    builder.Append("<File ");
                    builder.Append("ProductID = \"" + entity.ProductID + "\" ");
                    builder.Append("ProductName = \"" + entity.ProductName + "\" ");
                    builder.Append("Description = \"" + entity.Description + "\" ");
                    builder.Append("ProductPrefix = \"" + entity.ProductPrefix + "\" ");
                    builder.Append("Enabled = \"" + entity.Enabled + "\" ");
                    builder.Append("EndOfSupportDate = \"" + entity.EndOfSupportDate + "\" ");
                    builder.Append("BalanceType = \"" + entity.BalanceType + "\" ");
                    builder.Append("SubProductBalanceType = \"" + entity.SubProductBalanceType + "\" ");
                    builder.Append("GroupType = \"" + entity.GroupType + "\" ");
                    builder.Append("CasinoType = \"" + entity.CasinoType + "\" ");
                    builder.Append("IsOfferPlayForFun = \"" + entity.IsOfferPlayForFun + "\" ");
                    builder.Append("VendorID = \"" + entity.VendorID + "\" ");
                    builder.Append("OrderNo = \"" + entity.OrderNo + "\" ");
                    builder.Append("/>");
                }

                builder.Append("</ROOT>");
                return builder.ToString();
            }
        }

        public string LoginIP { get; set; }

        public int UserID { get; set; }

        public override string GetSettingKey() => "Acc_Product_Ins";
    }
}