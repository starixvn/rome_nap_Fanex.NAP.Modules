namespace Fanex.Tarus.Security.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Models;

    internal static class UrlPermissionManager
    {
        private static readonly IDictionary<string, UrlPermission> urlPermissions;

        static UrlPermissionManager()
        {
            urlPermissions = GetUrlPermissions();
        }

        internal static UrlPermission GetUrlPermission(string url)
        {
            UrlPermission urlPermission;
            urlPermissions.TryGetValue(url, out urlPermission);

            return urlPermission;
        }

        private static IDictionary<string, UrlPermission> GetUrlPermissions()
        {
            IDictionary<string, UrlPermission> urlDictionary = new Dictionary<string, UrlPermission>();

            var urlPermissionSection = Instance;
            var defaultParams = SplitByComma(urlPermissionSection.DefaultParams.ToLowerInvariant());

            foreach (UrlElement item in urlPermissionSection.Functions)
            {
                urlDictionary[item.Url.ToLowerInvariant()] = new UrlPermission(
                    ConcatUrlParams(defaultParams, item.Params.ToLowerInvariant()),
                    item.Policy,
                    SplitByComma(item.Filters));
            }

            return urlDictionary;
        }

        private static UrlPermissionSection Instance
            => ConfigurationManager.GetSection("UrlPermissionSection") as UrlPermissionSection;

        private static IEnumerable<string> ConcatUrlParams(IEnumerable<string> defaultParams, string parameter)
        {
            var parameters = SplitByComma(parameter);
            parameters.AddRange(defaultParams);

            return parameters;
        }

        private static List<string> SplitByComma(string value)
            => string.IsNullOrWhiteSpace(value)
                ? new List<string>()
                : value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
