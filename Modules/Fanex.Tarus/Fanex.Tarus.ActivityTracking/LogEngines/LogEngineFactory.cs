using Fanex.Logging;

namespace Fanex.Tarus.ActivityTracking.LogEngines
{
    internal interface LogEngineFactory
    {
        ILogEngine Create();
    }
}
