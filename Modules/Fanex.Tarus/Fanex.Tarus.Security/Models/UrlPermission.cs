namespace Fanex.Tarus.Security.Models
{
    using System.Collections.Generic;

    internal sealed class UrlPermission
    {
        internal UrlPermission(IEnumerable<string> paramNames, byte policy, IEnumerable<string> filters)
        {
            ParamNames = paramNames;
            Policy = policy;
            Filters = filters;
        }

        internal IEnumerable<string> ParamNames { get; }

        internal byte Policy { get; }

        internal IEnumerable<string> Filters { get; }
    }
}
