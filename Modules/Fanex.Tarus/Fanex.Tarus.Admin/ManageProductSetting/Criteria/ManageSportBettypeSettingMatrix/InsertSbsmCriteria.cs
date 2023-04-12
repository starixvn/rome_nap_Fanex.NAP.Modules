namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeSettingMatrix
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;
    using System.Collections.Generic;
    using System.Text;

    public class InsertSbsmCriteria : CriteriaBase
    {
        public InsertSbsmCriteria(IEnumerable<SbsmEntity> entities)
        {
            Entities = entities;
        }

        [SpParam(Ignored = true)]
        public IEnumerable<SbsmEntity> Entities { get; set; }

        public string InputData
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<ROOT>");
                foreach (var entity in Entities)
                {
                    builder.Append("<Setting ");
                    builder.Append("SportType = \"" + entity.SportType + "\" ");
                    builder.Append("BetType = \"" + entity.BetType + "\" ");
                    builder.Append("IsLive = \"" + entity.IsLive + "\" ");
                    builder.Append("IsLicensee = \"" + entity.IsLicensee + "\" ");
                    builder.Append("Site = \"" + entity.Site + "\" ");
                    builder.Append("SportTypeRefPT = \"" + entity.SportTypeRefPT + "\" ");
                    builder.Append("BetTypeRefPT = \"" + entity.BetTypeRefPT + "\" ");
                    builder.Append("IsLivePT = \"" + entity.IsLivePT + "\" ");
                    builder.Append("TablePT = \"" + entity.TablePT + "\" ");
                    builder.Append("SportTypeRefComm = \"" + entity.SportTypeRefComm + "\" ");
                    builder.Append("BetTypeRefComm = \"" + entity.BetTypeRefComm + "\" ");
                    builder.Append("TableComm = \"" + entity.TableComm + "\" ");
                    builder.Append("SportTypeRefBetSetting = \"" + entity.SportTypeRefBetSetting + "\" ");
                    builder.Append("BetTypeRefBetSetting = \"" + entity.BetTypeRefBetSetting + "\" ");
                    builder.Append("TableBetSetting = \"" + entity.TableBetSetting + "\" ");
                    builder.Append("TableBettrans = \"" + entity.TableBettrans + "\" ");
                    builder.Append("SporttypeRefWinlossLimit = \"" + entity.SporttypeRefWinlossLimit + "\" ");
                    builder.Append("BettypeRefWinlossLimit = \"" + entity.BettypeRefWinlossLimit + "\" ");
                    builder.Append("RealProductId = \"" + entity.ProductId + "\" ");
                    builder.Append("/>");
                }

                builder.Append("</ROOT>");
                return builder.ToString();
            }
        }


        public string IP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey() => "Acc_ReferenceSetting_Ins";
    }
}
