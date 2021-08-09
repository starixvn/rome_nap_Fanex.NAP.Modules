using System.Collections.Generic;
using Fanex.Tarus.Admin.Resource;

namespace Fanex.Tarus.Admin.Enums
{
    public static class UserLevelConstant
    {
        public const int Headline = 5;
        public const int Super = 4;
        public const int Master = 3;
        public const int Agent = 2;
        public const int Member = 1;

        public static readonly Dictionary<int, string> List = new Dictionary<int, string>
        {
            { Super, Language.Super},
            { Master, Language.Master},
            { Agent, Language.Agent},
            { Member, Language.Member}
        };
    }
}
