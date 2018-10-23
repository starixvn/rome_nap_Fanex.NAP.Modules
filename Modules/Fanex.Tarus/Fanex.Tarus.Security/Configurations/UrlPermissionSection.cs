namespace Fanex.Tarus.Security.Configurations
{
    using System;
    using System.Configuration;

    public sealed class UrlPermissionSection : ConfigurationSection
    {
        [ConfigurationProperty("defaultParams", IsRequired = true)]
        public string DefaultParams
        {
            get
            {
                return this["defaultParams"].ToString();
            }
            set
            {
                this["defaultParams"] = value;
            }
        }

        [ConfigurationProperty("Functions", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(UrlCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public UrlCollection Functions
            => (UrlCollection)base["Functions"];
    }

    public sealed class UrlCollection : ConfigurationElementCollection
    {
        protected override sealed ConfigurationElement CreateNewElement()
            => new UrlElement();

        protected override object GetElementKey(ConfigurationElement element)
            => ((UrlElement)element).Url;
    }

    public sealed class UrlElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true, IsKey = true)]
        public string Url
        {
            get
            {
                return this["url"].ToString().ToLowerInvariant();
            }
            set
            {
                this["url"] = value;
            }
        }

        [ConfigurationProperty("params", DefaultValue = "")]
        public string Params
        {
            get
            {
                return this["params"].ToString();
            }
            set
            {
                this["params"] = value;
            }
        }

        [ConfigurationProperty("policy", DefaultValue = "1")]
        public byte Policy
        {
            get
            {
                return Convert.ToByte(this["policy"]);
            }
            set
            {
                this["policy"] = value;
            }
        }

        [ConfigurationProperty("filters", DefaultValue = "")]
        public string Filters
        {
            get
            {
                return this["filters"].ToString();
            }
            set
            {
                this["filters"] = value;
            }
        }
    }
}
