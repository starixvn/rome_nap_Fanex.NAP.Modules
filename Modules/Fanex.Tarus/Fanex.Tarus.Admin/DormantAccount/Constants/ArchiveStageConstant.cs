using System.Collections.Generic;
using Fanex.Tarus.Admin.Resource;

namespace Fanex.Tarus.Admin.Enums
{
    public static class ArchiveStageConstant
    {
        public const int DeleteAccount = 4;
        public const int DeleteSetting = 3;
        public const int DisableAccount = 2;
        public const int CloseAccount = 1;

        public static readonly Dictionary<int, string> List = new Dictionary<int, string>
        {
            { CloseAccount, Language.CloseAccount},
            { DisableAccount, Language.DisableAccount},
            { DeleteSetting, Language.DeleteSetting},
            { DeleteAccount, Language.DeleteAccount}
        };
    }
}
