using System.Collections.Generic;
using Fanex.Tarus.Admin.Resource;

namespace Fanex.Tarus.Admin.Enums
{
    public static class ArchiveDateConstant
    {
        public const int DeleteAccountDate = 4;
        public const int DeleteSettingDate = 3;
        public const int DisableDate = 2;
        public const int CloseDate = 1;

        public static readonly Dictionary<int, string> List = new Dictionary<int, string>
        {
            { CloseDate, Language.CloseDate},
            { DisableDate, Language.DisableDate},
            { DeleteSettingDate, Language.DeleteSettingDate},
            { DeleteAccountDate, Language.DeleteAccountDate}
        };
    }
}
