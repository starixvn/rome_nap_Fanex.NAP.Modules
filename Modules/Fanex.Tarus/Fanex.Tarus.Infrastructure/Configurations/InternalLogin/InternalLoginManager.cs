namespace Fanex.Tarus.Infrastructure.Configurations.InternalLogin
{
    using System.Configuration;
    using System.Linq;
    using static Common.Enumerations;

    public static class InternalLoginManager
    {
        public static string GetInternalLoginUrl(string site, Role role = Role.Null)
        {
            var internalUrlSection = ConfigurationManager.GetSection("InternalUrlSection") as InternalUrlSection;
            site = site.Trim();
            foreach (InternalUrlElement element in internalUrlSection.InternalUrlCollection)
            {
                string[] configSites = element.Sites.Split(',');
                foreach (var configSite in configSites)
                {
                    if (configSite.Trim().Contains(site))
                    {
                        return role == Role.Headline ? element.UrlHeadline : element.Url;
                    }
                }
            }

            return (from InternalUrlElement e in internalUrlSection.InternalUrlCollection
                    where e.IsDefault
                    select e).FirstOrDefault()?.Url;
        }
    }
}
