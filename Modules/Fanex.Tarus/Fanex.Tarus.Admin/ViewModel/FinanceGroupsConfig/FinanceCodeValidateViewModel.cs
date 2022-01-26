namespace Fanex.Tarus.Admin.ViewModel.FinanceGroupsConfig
{
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using System.Collections.Generic;

    public class FinanceCodeValidateViewModel : Result
    {
        public IList<FinanceCodeValidateModel> FinanceCodes { get; set; } = new List<FinanceCodeValidateModel>();
        public byte IsAssignCodes { get; set; }
        public int NRows { get; set; }
    }
}
