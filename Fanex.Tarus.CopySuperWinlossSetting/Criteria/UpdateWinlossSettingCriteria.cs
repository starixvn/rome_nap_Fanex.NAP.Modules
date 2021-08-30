namespace Fanex.Tarus.CopySuperWinlossSetting.Criteria
{
    using Fanex.NAP.Framework.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UpdateWinlossSettingCriteria: CriteriaBase
    {
        public int UserId { get; set; }

        public int SubAccId => 0;

        public int CustId { get; set; }

        public string IP { get; set; }

        public string Limit { get; set; }

        public override bool IsValid()
        {
            return UserId > 0 && CustId > 0;
        }

        public override string GetSettingKey()
        {
            return "Acc_CustSetting_WinLossLimitUpd";
        }
    }
}
