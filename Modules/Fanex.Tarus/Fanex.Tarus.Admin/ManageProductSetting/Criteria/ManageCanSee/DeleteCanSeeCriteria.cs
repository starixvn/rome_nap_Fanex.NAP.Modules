namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSporttype
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;

    public class DeleteCanSeeCriteria : CriteriaBase
    {
        public DeleteCanSeeCriteria()
        {
            Entities = new List<CanSeeEntity>();
        }

        [SpParam(Ignored = true)]
        public List<CanSeeEntity> Entities { get; set; }

        public string FileSetting
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
                    builder.Append("Currency = \"" + entity.Currency + "\" ");
                    builder.Append("SiteID = \"" + entity.SiteId + "\" ");
                    builder.Append("/>");
                }

                builder.Append("</ROOT>");
                return builder.ToString();
            }
        }

        public string IP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public override string GetSettingKey() => "Acc_ManageCansee_Del";
    }
}