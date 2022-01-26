namespace Fanex.Tarus.Admin.ViewModel.FinanceGroupsConfig
{
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using System.Collections.Generic;

    public class FinanceUserValidateViewModel : Result
    {
        public IList<FinanceUserValidateModel> FinanceUsers { get; set; } = new List<FinanceUserValidateModel>();
        public byte IsAssignUsers { get; set; }
        public int NRows { get; set; }
    }
}
