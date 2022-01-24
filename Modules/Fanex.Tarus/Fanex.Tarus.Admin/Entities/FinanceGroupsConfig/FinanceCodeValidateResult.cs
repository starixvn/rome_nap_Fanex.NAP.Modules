namespace Fanex.Tarus.Admin.Entities.FinanceGroupsConfig
{
    using System.Collections.Generic;

    public class FinanceCodeValidateResult : Result
    {
        public IList<FinanceCodeValidateModel> FinanceCodes { get; set; } = new List<FinanceCodeValidateModel>();
        public byte IsAssignCodes { get; set; }
        public int NRows { get; set; }
    }
}
