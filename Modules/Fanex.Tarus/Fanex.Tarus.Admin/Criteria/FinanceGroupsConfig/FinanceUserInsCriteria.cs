namespace Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;
    using System.Collections.Generic;
    using System.Text;

    public class FinanceUserInsCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public List<FinanceUserValidateModel> FinanceUsers { get; set; } = new List<FinanceUserValidateModel>();

        public int GroupId { get; set; }

        public int NRows { get; set; }

        public string XmlData
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<root>");

                foreach (var user in FinanceUsers)
                {
                    builder.Append($"<r CustId=\"{user.CustId}\" Username=\"{user.UserName}\" RoleId=\"{user.RoleId}\" />");
                }

                builder.Append("</root>");
                return builder.ToString();
            }
        }

        public string IP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public override string GetSettingKey()
            => "Acc_FinanceGroupsConfig_UsersIns";
    }
}
