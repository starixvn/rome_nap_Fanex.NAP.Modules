namespace Fanex.Tarus.Admin.ManageUsernamePrefix.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class InsertCriteria : CriteriaBase
    {
        public string GroupName { get; set; }

        public string Prefix { get; set; }

        public int UserID { get; set; }

        public string IP { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override bool IsValid() => !string.IsNullOrWhiteSpace(GroupName)
            && !string.IsNullOrWhiteSpace(Prefix)
            && Prefix.Length == 2
            && UserID > 0
            && !string.IsNullOrWhiteSpace(IP);

        public override string GetSettingKey() => "Acc_SuperPefixIns";
    }
}
