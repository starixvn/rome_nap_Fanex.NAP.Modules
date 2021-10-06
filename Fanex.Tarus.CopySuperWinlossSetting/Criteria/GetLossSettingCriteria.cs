using System.Collections;
using Fanex.NAP.Framework.Data;
using Fanex.Tarus.CopySuperWinlossSetting.Constant;

namespace Fanex.Tarus.CopySuperWinlossSetting.Criteria
{
    internal class GetLossSettingCriteria : GetWinlossSettingCriteria
    {
        public GetLossSettingCriteria()
        {
            SettingType = DCSSettingType.LossLimit;
        }
    }
}
