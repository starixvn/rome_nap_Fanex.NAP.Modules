namespace Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.FinanceGroupsConfig;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;
    using System.Collections.Generic;
    using System.Text;

    public class FinanceGroupIsViewUpdCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public List<FinanceCodeSelectModel> FinanceCodes { get; set; } = new List<FinanceCodeSelectModel>();

        public string XmlData
        {
            get
            {
                var builder = new StringBuilder();

                builder.Append("<root>");
                foreach (var code in FinanceCodes)
                {
                    builder.Append($"<r GroupId=\"{code.GroupId}\" UserName=\"{code.FinanceCode}\" IsView=\"{code.IsView}\"/>");
                }
                builder.Append("</root>");

                return builder.ToString();
            }
        }

        public string IP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public override string GetSettingKey() => "Acc_FinanceGroupsConfig_FinanceCode_IsViewUpd";
    }
}
