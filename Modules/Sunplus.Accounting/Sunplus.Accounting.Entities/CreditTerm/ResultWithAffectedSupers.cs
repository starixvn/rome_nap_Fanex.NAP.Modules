namespace Sunplus.Accounting.Entities.CreditTerm
{
    public class ResultWithAffectedSupers : Result
    {
        public ResultWithAffectedSupers()
        {
        }

        public ResultWithAffectedSupers(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }

        public string AffectedSupers { get; set; }
    }
}
