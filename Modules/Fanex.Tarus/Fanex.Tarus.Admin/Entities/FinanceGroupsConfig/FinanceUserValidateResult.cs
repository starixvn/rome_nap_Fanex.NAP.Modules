namespace Fanex.Tarus.Admin.Entities.FinanceGroupsConfig
{
    using System.Collections.Generic;

    public class FinanceUserValidateResult : Result
    {
        public IList<FinanceUserValidateModel> FinanceUsers { get; set; } = new List<FinanceUserValidateModel>();
        public byte IsAssignUsers { get; set; }
        public int NRows { get; set; }
    }
}
