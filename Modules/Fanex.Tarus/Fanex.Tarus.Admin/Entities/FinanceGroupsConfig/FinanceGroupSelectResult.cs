namespace Fanex.Tarus.Admin.Entities.FinanceGroupsConfig
{
    using System.Collections.Generic;

    public class FinanceGroupSelectResult
    {
        public IList<FinanceUserSelectModel> FinanceUsers { get; set; } = new List<FinanceUserSelectModel>();
        public IList<FinanceCodeSelectModel> FinanceCodes { get; set; } = new List<FinanceCodeSelectModel>();
        public int TotalItems { get; set; }
    }
}
