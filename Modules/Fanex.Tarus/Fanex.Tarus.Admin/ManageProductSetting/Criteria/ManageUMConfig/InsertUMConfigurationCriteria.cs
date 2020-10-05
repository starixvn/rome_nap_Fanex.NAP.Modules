namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig
{
    using Fanex.NAP.Framework.Data;

    public class InsertUMConfigurationCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public string IP { get; set; }

        public int FunctionType { get; set; }

        public int ProductId { get; set; }

        public int SportType { get; set; }

        public int SettingType { get; set; }

        public int SiteId { get; set; }

        public int Currency { get; set; }

        public string Usernames { get; set; }

        public string BeginTime { get; set; }

        public string EndTime { get; set; }

        public string ShowTime { get; set; }

        public bool LockAddingNew { get; set; }

        public int BetType { get; set; }

        public bool IsLive { get; set; }

        public override string GetSettingKey()
            => "Acc_UMTool_Configuration_Ins";

        public override bool IsValid()
            => UserId > 0
            && !string.IsNullOrWhiteSpace(BeginTime)
            && !string.IsNullOrWhiteSpace(EndTime)
            && !string.IsNullOrWhiteSpace(ShowTime);
    }
}