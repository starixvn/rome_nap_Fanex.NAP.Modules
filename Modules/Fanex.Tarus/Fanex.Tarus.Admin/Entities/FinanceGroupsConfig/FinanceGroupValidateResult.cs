namespace Fanex.Tarus.Admin.Entities.FinanceGroupsConfig
{
    using System.Collections.Generic;

    public class FinanceGroupValidateResult : Result
    {
        public IList<FinanceUserValidateModel> FinanceUsers { get; set; } = new List<FinanceUserValidateModel>();
        public IList<FinanceCodeValidateModel> FinanceCodes { get; set; } = new List<FinanceCodeValidateModel>();
        public byte IsCreateGroup { get; set; } = 1;
    }
}
