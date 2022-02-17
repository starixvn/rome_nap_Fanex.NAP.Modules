namespace Fanex.Tarus.Admin.Requests.FinanceGroupsConfig
{
    public class FinanceGroupDeleteAllRequest
    {
        public string GroupIds { get; set; }
        public string UsernameList { get; set; }
        public int? Level { get; set; }
        public int? IsView { get; set; }
        public int TotalItems { get; set; }
    }
}
