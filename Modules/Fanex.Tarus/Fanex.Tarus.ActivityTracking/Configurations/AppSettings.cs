using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Fanex.Tarus.ActivityTracking.Configurations
{
    internal static class AppSettings
    {
        [ThreadStatic]
        private static NameValueCollection appSettingValues;

        static AppSettings()
        {
            appSettingValues = ConfigurationManager.AppSettings;
        }

        public static string LoggingDefaultCategory => appSettingValues["Fanex.Logging:DefaultCategory"];

        public static string LogLibs => appSettingValues["LogLibs"];

        public static string RabbitMQConnectionStrings => appSettingValues["Fanex.Logging:RabbitMQConnectionStrings"];

        public static string SentryBackendUrl => appSettingValues["Fanex.Logging:SentryBackendUrl"];

        public static string SentryUrl => appSettingValues["Fanex.Logging:SentryUrl"];

        public static string RavenLibPath => appSettingValues["RavenLibPath"];
    }
}
