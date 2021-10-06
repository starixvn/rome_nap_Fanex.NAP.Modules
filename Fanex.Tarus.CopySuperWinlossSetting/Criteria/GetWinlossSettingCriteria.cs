using System.Collections;
using Fanex.NAP.Framework.Data;
using Fanex.Tarus.CopySuperWinlossSetting.Constant;

namespace Fanex.Tarus.CopySuperWinlossSetting.Criteria
{
    public class GetWinlossSettingCriteria: CriteriaBase
    {
        public int CustId { get; set; }

        public int ProductId { get; set; }

        public int SettingType { get; set; }

        public override bool IsValid()
        {
            return CustId > 0 && ProductId > 0;
        }

        public override string GetSettingKey()
        {
            return "Acc_CustSetting_WinLossLimitSelect";
        }
    }
}
