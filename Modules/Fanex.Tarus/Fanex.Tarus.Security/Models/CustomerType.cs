namespace Fanex.Tarus.Security.Models
{
    public sealed class CustomerType
    {
        public CustomerType(string custInfo, bool isDeposit, bool isValid, bool isInternal)
        {
            CustInfo = custInfo;
            IsDeposit = isDeposit;
            IsValid = isValid;
            IsInternal = isInternal;
        }

        public string CustInfo { get; }

        public bool IsDeposit { get; }

        public bool IsValid { get; }

        public bool IsInternal { get; }
    }
}
