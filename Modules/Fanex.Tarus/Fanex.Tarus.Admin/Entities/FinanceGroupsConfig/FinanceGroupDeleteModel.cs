namespace Fanex.Tarus.Admin.Entities.FinanceGroupsConfig
{
    public class FinanceGroupDeleteModel
    {
        public string GroupId { get; set; }
        public string UserName { get; set; }
        public byte IsView { get; set; }
        public int RoleId { get; set; }
    }
}
