namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSporttype
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;

    public class InsertSportTypeCriteria : CriteriaBase
    {
        public InsertSportTypeCriteria()
        {
            Entities = new List<SportTypeEntity>();
        }

        [SpParam(Ignored = true)]
        public List<SportTypeEntity> Entities { get; set; }

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
                    builder.Append("SportName = \"" + entity.SportName + "\" ");
                    builder.Append("SportNameCh = \"" + entity.SportNameCh + "\" ");
                    builder.Append("SportNameCs = \"" + entity.SportNameCs + "\" ");
                    builder.Append("SportNameJp = \"" + entity.SportNameJp + "\" ");
                    builder.Append("SportNameIt = \"" + entity.SportNameIt + "\" ");
                    builder.Append("SportNameTh = \"" + entity.SportNameTh + "\" ");
                    builder.Append("ParlayMaxPayout = \"" + entity.ParlayMaxPayout + "\" ");
                    builder.Append("SportNameKo = \"" + entity.SportNameKo + "\" ");
                    builder.Append("SportNameES = \"" + entity.SportNameES + "\" ");
                    builder.Append("SportNameVN = \"" + entity.SportNameVN + "\" ");
                    builder.Append("SportNameEL = \"" + entity.SportNameEL + "\" ");
                    builder.Append("SportNameRU = \"" + entity.SportNameRU + "\" ");
                    builder.Append("SportNameID = \"" + entity.SportNameID + "\" ");
                    builder.Append("SportNamePT = \"" + entity.SportNamePT + "\" ");
                    builder.Append("SportNameFR = \"" + entity.SportNameFR + "\" ");
                    builder.Append("SportNameMMR = \"" + entity.SportNameMMR + "\" ");
                    builder.Append("SportNamePL = \"" + entity.SportNamePL + "\" ");
                    builder.Append("SportNameHI = \"" + entity.SportNameHI + "\" ");
                    builder.Append("/>");
                }

                builder.Append("</ROOT>");
                return builder.ToString();
            }
        }

        public string LoginIP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public override string GetSettingKey() => "Acc_Sporttype_Ins";
    }
}