using Fanex.Logging.Extensions.Web;

namespace Fanex.Tarus.ActivityTracking.Web
{
    internal class ExceptionHandlerAdapter
    {
        public virtual LoggingConfiguration UseConfig(LoggingConfiguration loggingConfiguration)
            => ExceptionHandler.UseConfig(loggingConfiguration);
    }
}
