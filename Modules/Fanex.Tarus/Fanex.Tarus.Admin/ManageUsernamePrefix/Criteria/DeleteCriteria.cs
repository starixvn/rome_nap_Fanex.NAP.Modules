namespace Fanex.Tarus.Admin.ManageUsernamePrefix.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class DeleteCriteria : CriteriaBase
    {
        public string GroupName { get; set; }

        public string Prefix { get; set; }

        public int UserID { get; set; }

        public string IP { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override bool IsValid() => !string.IsNullOrWhiteSpace(GroupName)
            && UserID > 0
            && !string.IsNullOrWhiteSpace(IP);

        public override string GetSettingKey() => "Acc_SuperPrefixDel";
    }
}
