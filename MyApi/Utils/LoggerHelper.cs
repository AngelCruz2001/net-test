using Microsoft.Extensions.Logging;
using System;

namespace MyApi.Utils
{
    public static class LoggerHelper
    {
        public static void LogInfo(ILogger logger, string message)
        {
            logger.LogInformation($"[INFO] {message}");
        }

        public static void LogError(ILogger logger, string message, Exception ex = null)
        {
            logger.LogError(ex, $"[ERROR] {message}");
        }
    }
}
