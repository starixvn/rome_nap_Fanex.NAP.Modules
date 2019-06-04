namespace Fanex.Tarus.Infrastructure.Configurations.InternalLogin
{
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using static Common.Enumerations;

    public static class InternalLoginManager
    {
        public static string GetInternalLoginUrl(string site, Role role = Role.Null)
        {
            var internalUrlSection = ConfigurationManager.GetSection("InternalUrlSection") as InternalUrlSection;
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            foreach (InternalUrlElement element in internalUrlSection.InternalUrlCollection)
            {
                if (culture.CompareInfo.IndexOf(element.Sites, site, CompareOptions.IgnoreCase) >= 0)
                {
                    return role == Role.Headline ? element.UrlHeadline : element.Url;
                }
            }

            return (from InternalUrlElement e in internalUrlSection.InternalUrlCollection
                    where e.IsDefault
                    select e).FirstOrDefault()?.Url;
        }
    }
}
