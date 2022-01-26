namespace Fanex.Tarus.Admin.ViewModel.FinanceGroupsConfig
{
    using Fanex.Tarus.Admin.Entities;

    public class FinanceGroupInsertViewModel : Result
    {
        public int GroupId { get; set; }
        public int NUsers { get; set; }
        public int NCodes { get; set; }
    }
}
