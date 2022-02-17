namespace Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;
    using System.Collections.Generic;
    using System.Text;

    public class FinanceGroupInsCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public List<FinanceUserValidateModel> FinanceUsers { get; set; } = new List<FinanceUserValidateModel>();

        [SpParam(Ignored =true)]
        public List<FinanceCodeValidateModel> FinanceCodes { get; set; } = new List<FinanceCodeValidateModel>();

        public int GroupId { get; set; }

        public int NUsers { get; set; }

        public int NCodes { get; set; }

        public string IP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public string XmlData
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<root>");

                foreach(var user in FinanceUsers)
                {
                    builder.Append($"<r CustId=\"{user.CustId}\" UserName=\"{user.UserName}\" RoleId=\"{user.RoleId}\" IsView=\"0\"  />");
                }
                foreach(var code in FinanceCodes)
                {
                    builder.Append($"<r CustId=\"{code.CustId}\" UserName=\"{code.UserName}\" RoleId=\"{code.RoleId}\" IsView=\"{code.IsView}\"  />");
                }

                builder.Append("</root>");
                return builder.ToString();
            }
        }

        public override string GetSettingKey()
            => "Acc_FinanceGroupsConfig_FinanceGroupIns";
    }
}
