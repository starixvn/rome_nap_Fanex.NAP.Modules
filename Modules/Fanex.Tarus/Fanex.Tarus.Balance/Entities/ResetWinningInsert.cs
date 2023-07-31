namespace Fanex.Tarus.Balance.Entities
{
    public class ResetWinningInsert
    {
        public string WinLossDate { get; set; }

        public int CustId { get; set; }

        public decimal Amount { get; set; }

        public string TransDesc { get; set; }

        public int Creator { get; set; }

        public string Ip { get; set; }        
    }
}