namespace Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;

    public class FinanceGroupIsViewUpdCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public string GroupId { get; set; }

        [SpParam(Ignored = true)]
        public string UserName { get; set; }

        [SpParam(Ignored = true)]
        public int IsView { get; set; }

        public string XmlData
        {
            get
            {
                return $"<root><r GroupId=\"{GroupId}\" UserName=\"{UserName}\" IsView=\"{IsView}\"/></root>";
            }
        }

        public string IP { get; } = Utils.GetClientIP();

        public int UserID { get; } = AppSession.AccId;

        public override string GetSettingKey() => "Acc_FinanceGroupsConfig_FinanceGroup_IsViewUpd";
    }
}
