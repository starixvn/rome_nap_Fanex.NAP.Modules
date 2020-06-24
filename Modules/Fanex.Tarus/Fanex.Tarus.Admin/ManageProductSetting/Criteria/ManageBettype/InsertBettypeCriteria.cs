namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageBettype
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;

    public class InsertBettypeCriteria : CriteriaBase
    {
        public InsertBettypeCriteria()
        {
            Entities = new List<BettypeEntity>();
        }

        [SpParam(Ignored = true)]
        public List<BettypeEntity> Entities { get; set; }

        public string File
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<ROOT>");
                foreach (var entity in Entities)
                {
                    builder.Append("<File ");
                    builder.Append("typeid = \"" + entity.TypeId + "\" ");
                    builder.Append("typenamee = \"" + entity.TypeNamee + "\" ");
                    builder.Append("typenamec = \"" + entity.TypeNamec + "\" ");
                    builder.Append("typedesc = \"" + entity.TypeDesc + "\" ");
                    builder.Append("typenamec_uni = \"" + entity.TypeNamec_uni + "\" ");
                    builder.Append("typenamej = \"" + entity.TypeNamej + "\" ");
                    builder.Append("typenamcn = \"" + entity.TypeNamcn + "\" ");
                    builder.Append("typenamecn = \"" + entity.TypeNamecn + "\" ");
                    builder.Append("typenameth = \"" + entity.TypeNameth + "\" ");
                    builder.Append("typenameit = \"" + entity.TypeNameit + "\" ");
                    builder.Append("typenameko = \"" + entity.TypeNameko + "\" ");
                    builder.Append("typenameES = \"" + entity.TypeNameES + "\" ");
                    builder.Append("typenameVN = \"" + entity.TypeNameVN + "\" ");
                    builder.Append("typenameEL = \"" + entity.TypeNameEL + "\" ");
                    builder.Append("typenamee_Eu = \"" + entity.TypeNamee_Eu + "\" ");
                    builder.Append("typenameRU = \"" + entity.TypeNameRU + "\" ");
                    builder.Append("typenameID = \"" + entity.TypeNameID + "\" ");
                    builder.Append("typenamePT = \"" + entity.TypeNamePT + "\" ");
                    builder.Append("typenameFR = \"" + entity.TypeNameFR + "\" ");
                    builder.Append("ParentTypeid = \"" + entity.ParentTypeid + "\" ");
                    builder.Append("typenameMMR = \"" + entity.TypeNameMMR + "\" ");
                    builder.Append("typenameHI = \"" + entity.TypeNameHI + "\" ");
                    builder.Append("DisplayedTypeID = \"" + entity.DisplayedTypeID + "\" ");
                    builder.Append("/>");
                }

                builder.Append("</ROOT>");
                return builder.ToString();
            }
        }

        public string LoginIP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public override string GetSettingKey() => "Acc_Bettype_Ins";
    }
}