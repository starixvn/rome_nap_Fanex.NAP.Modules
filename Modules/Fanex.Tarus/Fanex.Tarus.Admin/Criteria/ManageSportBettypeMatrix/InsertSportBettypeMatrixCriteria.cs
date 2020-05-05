using System.Collections.Generic;
using System.Text;
using Fanex.NAP.Framework.Data;
using Fanex.Tarus.Admin.Entities.ManageSportBettypeMatrix;

namespace Fanex.Tarus.Admin.Criteria.ManageSportBettypeMatrix
{
    public class InsertSportBettypeMatrixCriteria : CriteriaBase
    {
        public InsertSportBettypeMatrixCriteria()
        {
            Entities = new List<SportBettypeMatrixEntity>();
        }

        [SpParam(Ignored = true)]
        public List<SportBettypeMatrixEntity> Entities { get; set; }

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
                    builder.Append("ProductId = \"" + entity.ProductId + "\" ");
                    builder.Append("Enabled = \"" + entity.Enabled + "\" ");
                    builder.Append("IsTest = \"" + entity.IsTest + "\" ");
                    builder.Append("EventType = \"" + entity.EventType + "\" ");
                    builder.Append("SubProduct = \"" + entity.SubProduct + "\" ");
                    builder.Append("RefTable = \"" + entity.RefTable + "\" ");
                    builder.Append("/>");
                }

                builder.Append("</ROOT>");
                return builder.ToString();
            }
        }

        public string LoginIP { get; set; }

        public int UserID { get; set; }

        public override string GetSettingKey() => "Acc_SportBettypeMatrix_Ins";
    }
}