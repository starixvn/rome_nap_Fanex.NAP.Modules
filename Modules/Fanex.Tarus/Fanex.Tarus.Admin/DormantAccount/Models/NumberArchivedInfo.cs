namespace Fanex.Tarus.Admin.DormantAccount.Models
{
    public class NumberArchivedInfo
    {
        public int Close { get; set; }

        public int Disable { get; set; }

        public int DelSetting { get; set; }

        public int DelAccount { get; set; }

        public string DisplayText => $"{Close}   {Disable}   {DelSetting}   {DelAccount}";
    }
}
