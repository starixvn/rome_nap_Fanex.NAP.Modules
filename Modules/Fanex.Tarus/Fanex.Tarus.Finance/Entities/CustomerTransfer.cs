namespace Fanex.Tarus.Finance.Entities
{
    internal class CustomerTransfer
    {
        public CustomerTransfer(
            int fromCustId,
            string fromUsername,
            int toCustId,
            string toUsername,
            decimal amount,
            double exchangeRate,
            string description,
            bool isCollection,
            int userId,
            string ip)
        {
            FromCustId = fromCustId;
            FromUsername = fromUsername;
            ToCustId = toCustId;
            ToUsername = toUsername;
            Amount = amount;
            ExchangeRate = exchangeRate;
            Description = description;
            IsCollection = isCollection;
            UserId = userId;
            IP = ip;
        }

        public int FromCustId { get; }

        public string FromUsername { get; }

        public int ToCustId { get; }

        public string ToUsername { get; }

        public decimal Amount { get; }

        public double ExchangeRate { get; }

        public string Description { get; }

        public bool IsCollection { get; }

        public int UserId { get; }

        public string IP { get; }
    }
}
