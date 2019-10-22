using Fanex.Logging;

namespace Fanex.Tarus.ActivityTracking.Web
{
    internal class LogManagerAdapter : LogManager
    {
        public new virtual LogManager SetDefaultLogCategory(string defaultCategory)
            => LogManager.SetDefaultLogCategory(defaultCategory);
    }
}
