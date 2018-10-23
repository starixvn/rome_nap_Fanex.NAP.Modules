namespace Fanex.Tarus.Security.Models
{
    using System.Collections.Generic;

    internal sealed class UrlPermission
    {
        public UrlPermission(IEnumerable<string> paramNames, byte policy, IEnumerable<string> filters)
        {
            ParamNames = paramNames;
            Policy = policy;
            Filters = filters;
        }

        public IEnumerable<string> ParamNames { get; }

        public byte Policy { get; }

        public IEnumerable<string> Filters { get; }
    }
}
