namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeMatrix
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using System.Collections.Generic;
    using System.Text;

    public class DeleteSportBettypeMatrixCriteria : CriteriaBase
    {
        public DeleteSportBettypeMatrixCriteria(IEnumerable<SportBettypeMatrixEntity> entities)
        {
            Entities = entities;
        }

        [SpParam(Ignored = true)]
        public IEnumerable<SportBettypeMatrixEntity> Entities { get; set; }

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
                    builder.Append("/>");
                }

                builder.Append("</ROOT>");

                return builder.ToString();
            }
        }

        public int UserId { get; set; }

        public string LoginIp { get; set; }

        public override string GetSettingKey() => "Acc_SportBettypeMatrix_Del";
    }
}