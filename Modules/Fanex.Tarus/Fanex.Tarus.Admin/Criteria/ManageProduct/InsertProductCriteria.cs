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
                    builder.Append("productID = \"" + entity.ProductID + "\" ");
                    builder.Append("productName = \"" + entity.ProductName + "\" ");
                    builder.Append("description = \"" + entity.Description + "\" ");
                    builder.Append("productPrefix = \"" + entity.ProductPrefix + "\" ");
                    builder.Append("enabled = \"" + entity.Enabled + "\" ");
                    builder.Append("endOfSupportDate = \"" + entity.EndOfSupportDate + "\" ");
                    builder.Append("balanceType = \"" + entity.BalanceType + "\" ");
                    builder.Append("subProductBalanceType = \"" + entity.SubProductBalanceType + "\" ");
                    builder.Append("groupType = \"" + entity.GroupType + "\" ");
                    builder.Append("casinoType = \"" + entity.CasinoType + "\" ");
                    builder.Append("isOfferPlayForFun = \"" + entity.IsOfferPlayForFun + "\" ");
                    builder.Append("vendorID = \"" + entity.VendorID + "\" ");
                    builder.Append("orderNo = \"" + entity.OrderNo + "\" ");
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