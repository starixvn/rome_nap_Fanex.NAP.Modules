namespace Fanex.Tarus.Admin.ManageProductSetting.Entities.UMConfig
{
    public class UMConfigurationEntity
    {
        public int ConfigId { get; set; }
        public int FunctionType { get; set; }
        public int ProductId { get; set; }
        public int SportType { get; set; }
        public int BetType { get; set; }
        public bool IsLive { get; set; }
        public int SettingType { get; set; }
        public int SiteId { get; set; }
        public int Currency { get; set; }
        public int SuperId { get; set; }
        public string FunctionTypeName { get; set; }
        public string ProductName { get; set; }
        public string SportName { get; set; }
        public string SettingName { get; set; }
        public string Site { get; set; }
        public string CurrencyName { get; set; }
        public string Username { get; set; }
        public string ShowTime { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public bool LockAddingNew { get; set; }
    }
}
