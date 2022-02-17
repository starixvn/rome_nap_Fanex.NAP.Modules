namespace Fanex.Tarus.Admin.ViewModel.FinanceGroupsConfig
{
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.FinanceGroupsConfig;
    using System.Collections.Generic;

    public class FinanceGroupSelectViewModel
    {
        public IList<FinanceUserSelectModel> FinanceUsers { get; set; } = new List<FinanceUserSelectModel>();
        public IList<FinanceCodeSelectModel> FinanceCodes { get; set; } = new List<FinanceCodeSelectModel>();
        public int TotalItems { get; set; }
    }
}
