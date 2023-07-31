
using Fanex.NAP.Framework.Data;
using Fanex.Tarus.Balance.Entities;

namespace Fanex.Tarus.Balance.Criteria
{
    public class AddResetWinningCriteria : CriteriaBase
    {
        public AddResetWinningCriteria(ResetWinningInsert model, int siteDBGroupId)
        {
            WinlostDate = model.WinLossDate;
            CustId = model.CustId;
            Amount = model.Amount;
            TransDesc = model.TransDesc;
            Creator = model.Creator;
            IP = model.Ip;
            SiteDBGroupId = siteDBGroupId;
        }

        [SpParam(Ignored = true)]
        private int SiteDBGroupId { get; set; }

        public string WinlostDate { get; set; }

        public int CustId { get; set; }

        public decimal Amount { get; set; }

        public string TransDesc { get; set; }

        public int Creator { get; set; }

        public string IP { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public virtual string GetSettingKeys()
            => SiteDBGroupId > 0 ? $"SiteDB{SiteDBGroupId}_Task_ResetWinning_Non3rdParty_Add" : "Task_ResetWinning_Non3rdParty_Add";
    }
}
