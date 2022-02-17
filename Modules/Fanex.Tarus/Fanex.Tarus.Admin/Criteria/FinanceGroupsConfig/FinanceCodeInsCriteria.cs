namespace Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;
    using System.Collections.Generic;
    using System.Text;

    public class FinanceCodeInsCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public List<FinanceCodeValidateModel> FinanceCodes { get; set; } = new List<FinanceCodeValidateModel>();

        public int GroupId { get; set; }

        public int NRows { get; set; }

        public string XmlData
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<root>");

                foreach (var code in FinanceCodes)
                {
                    builder.Append($"<r CustId=\"{code.CustId}\" UserName=\"{code.UserName}\" RoleId=\"{code.RoleId}\" IsView=\"{code.IsView}\" />");
                }

                builder.Append("</root>");
                return builder.ToString();
            }
        }

        public string IP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public override string GetSettingKey()
            => "Acc_FinanceGroupsConfig_CodesIns";
    }
}
