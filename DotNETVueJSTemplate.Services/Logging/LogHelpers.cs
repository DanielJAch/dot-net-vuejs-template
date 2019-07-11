using NLog;

namespace DotNETVueJSTemplate.Services
{
    public static class LogHelpers
    {
        public static void SkipEmptyLogEntry(this ILogger logger, LogLevel level, string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                return;
            }

            logger.Log(level, msg);
        }
    }
}
