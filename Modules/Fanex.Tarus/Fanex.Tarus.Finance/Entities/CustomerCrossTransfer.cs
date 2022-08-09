namespace Fanex.Tarus.Finance.Entities
{
    internal class CustomerCrossTransfer
    {
        public CustomerCrossTransfer(
            int fromCustId,
            string fromUsername,
            int toCustId,
            string toUsername,
            decimal fromAmount,
            decimal toAmount,
            string remark,
            bool isCollection,
            string forexUsername,
            int userId,
            string ip)
        {
            FromCustId = fromCustId;
            FromUsername = fromUsername;
            ToCustId = toCustId;
            ToUsername = toUsername;
            FromAmount = fromAmount;
            ToAmount = toAmount;
            Remark = remark;
            IsCollection = isCollection;
            ForexUsername = forexUsername;
            UserId = userId;
            IP = ip;
        }

        public int FromCustId { get; }

        public string FromUsername { get; }

        public int ToCustId { get; }

        public string ToUsername { get; }

        public decimal FromAmount { get; }

        public decimal ToAmount { get; }

        public string Remark { get; }

        public bool IsCollection { get; }

        public string ForexUsername { get; }

        public int UserId { get; }

        public string IP { get; }
    }
}
