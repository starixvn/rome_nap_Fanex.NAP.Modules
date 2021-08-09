using System.Collections.Generic;
using Fanex.Tarus.Admin.Resource;

namespace Fanex.Tarus.Admin.Enums
{
    public static class HeadlineTypeConstant
    {
        public const int Normal = 0;
        public const int VIP = 1;

        public static readonly Dictionary<int, string> List = new Dictionary<int, string>
        {
            { Normal, Language.Normal},
            { VIP, Language.VIP}
        };
    }
}
