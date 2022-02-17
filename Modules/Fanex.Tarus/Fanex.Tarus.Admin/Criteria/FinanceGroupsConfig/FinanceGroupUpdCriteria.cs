namespace Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;

    public class FinanceGroupUpdCriteria : CriteriaBase
    {
        public int? NewGroupId { get; set; }

        public int? OldGroupId { get; set; }

        public string UserName { get; set; }

        public string IP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public override string GetSettingKey()
            => "Acc_FinanceGroupsConfig_FinanceUser_GroupUpd";
    }
}
