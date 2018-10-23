namespace Fanex.Tarus.Security.Models
{
    internal sealed class CustomerType
    {
        public CustomerType(string custInfo, bool isDeposit, bool isValid)
        {
            CustInfo = custInfo;
            IsDeposit = isDeposit;
            IsValid = isValid;
        }

        public string CustInfo { get; }

        public bool IsDeposit { get; }

        public bool IsValid { get; }
    }
}
