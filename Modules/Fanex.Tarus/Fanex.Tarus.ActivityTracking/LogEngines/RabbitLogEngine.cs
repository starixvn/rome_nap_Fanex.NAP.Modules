using Fanex.Logging;
using Fanex.Logging.RabbitMQ;
using Fanex.Tarus.ActivityTracking.Configurations;

namespace Fanex.Tarus.ActivityTracking.LogEngines
{
    internal class RabbitLogEngine : LogEngineFactory
    {
        public ILogEngine Create()
            => new RabbitMQLogging(AppSettings.RabbitMQConnectionStrings);
    }
}
