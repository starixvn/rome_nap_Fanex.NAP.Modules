namespace Fanex.Tarus.Admin.Entities.CompareBalance
{
    public class CompareBalanceModel
    {
        public string UserName { get; set; }

        public decimal TotalCash { get; set; }

        public decimal OutStanding { get; set; }

        public decimal CreditSports { get; set; }

        public decimal DebitSports { get; set; }

        public decimal CreditCasino { get; set; }

        public decimal DebitCasino { get; set; }

        public decimal CreditOthers { get; set; }

        public decimal DebitOthers { get; set; }

        public decimal CreditTransfer { get; set; }

        public decimal DebitTransfer { get; set; }

        public decimal CreditNetPosition { get; set; }

        public decimal DebitNetPosition { get; set; }

        public decimal CreditBonusWallet { get; set; }

        public decimal DebitBonusWallet { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public decimal TotalCashGeneralWallet
        {
            get
            {
                return CreditSports + CreditCasino + CreditOthers + CreditTransfer
                    - DebitSports - DebitCasino - DebitOthers - DebitTransfer;
            }
        }
        public decimal TotalCashBonusWallet
        {
            get
            {
                return CreditBonusWallet - DebitBonusWallet;
            }
        }

        public decimal TotalLeftPanel
        {
            get
            {
                return TotalCash - OutStanding;
            }
        }

        public decimal TotalNetPosition
        {
            get
            {
                return CreditNetPosition - DebitNetPosition;
            }
        }
    }
}
