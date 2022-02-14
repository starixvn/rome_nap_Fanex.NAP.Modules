namespace Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;
    using System.Collections.Generic;
    using System.Text;

    public class FinanceGroupDelCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public IList<FinanceGroupDeleteModel> FinanceGroups { get; set; } = new List<FinanceGroupDeleteModel>();

        public string XmlData
        {
            get
            {
                var builder =  new StringBuilder();

                builder.Append("<root>");
                foreach(var group in FinanceGroups)
                {
                    builder.Append($"<r GroupId=\"{group.GroupId}\" UserName=\"{group.UserName}\" IsView=\"{group.IsView}\" RoleId=\"{group.RoleId}\"  />");
                }
                builder.Append("</root>");

                return builder.ToString();
            }
        }

        public string IP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public override string GetSettingKey() => "Acc_FinanceGroupsConfig_FinanceCodeDel";
    }
}
