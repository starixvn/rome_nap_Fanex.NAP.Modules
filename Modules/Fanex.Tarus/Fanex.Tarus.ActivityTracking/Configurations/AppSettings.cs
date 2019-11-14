using System.Configuration;

namespace Fanex.Tarus.ActivityTracking.Configurations
{
    internal static class AppSettings
    {
        static AppSettings()
        {
            var appSettingValues = ConfigurationManager.AppSettings;

            LoggingDefaultCategory = appSettingValues["Fanex.Logging:DefaultCategory"];
            LogLibs = appSettingValues["LogLibs"];
            RabbitMQConnectionStrings = appSettingValues["Fanex.Logging:RabbitMQConnectionStrings"];
            SentryBackendUrl = appSettingValues["Fanex.Logging:SentryBackendUrl"];
            SentryUrl = appSettingValues["Fanex.Logging:SentryUrl"];
            RavenLibPath = appSettingValues["RavenLibPath"];
        }

        public static string LoggingDefaultCategory { get; }

        public static string LogLibs { get; }

        public static string RabbitMQConnectionStrings { get; }

        public static string SentryBackendUrl { get; }

        public static string SentryUrl { get; }

        public static string RavenLibPath { get; }
    }
}
