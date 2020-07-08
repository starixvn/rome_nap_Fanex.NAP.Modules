namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageCSSportBettypeMatrix
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;

    public class InsertCSSportBettypeMatrixCriteria : CriteriaBase
    {
        public InsertCSSportBettypeMatrixCriteria()
        {
            Entities = new List<CSSportBettypeMatrixEntity>();
        }

        [SpParam(Ignored = true)]
        public List<CSSportBettypeMatrixEntity> Entities { get; set; }

        public string File
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<ROOT>");
                foreach (var entity in Entities)
                {
                    builder.Append("<File ");
                    builder.Append("SportType = \"" + entity.SportType + "\" ");
                    builder.Append("BetType = \"" + entity.BetType + "\" ");
                    builder.Append("IsLive = \"" + entity.IsLive + "\" ");
                    builder.Append("ProductID = \"" + entity.ProductId + "\" ");
                    builder.Append("Enabled = \"" + entity.Enabled + "\" ");
                    builder.Append("IsTest = \"" + entity.IsTest + "\" ");
                    builder.Append("EventType = \"" + entity.EventType + "\" ");
                    builder.Append("SubProduct = \"" + entity.SubProduct + "\" ");
                    builder.Append("RefTable = \"" + entity.RefTable + "\" ");
                    builder.Append("SportTypeGroup = \"" + entity.SportTypeGroupId + "\" ");
                    builder.Append("BetTypeGroup = \"" + entity.BetTypeGroupId + "\" ");
                    builder.Append("/>");
                }

                builder.Append("</ROOT>");
                return builder.ToString();
            }
        }

        public string LoginIP { get; set; }

        public int UserID { get; set; }

        public override string GetSettingKey() => "Acc_CS_SportBettypeMatrix_Ins";
    }
}