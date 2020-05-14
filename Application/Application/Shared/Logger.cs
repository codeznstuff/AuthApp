using Microsoft.Extensions.Logging;
using System;

namespace Shared
{
    public class Logger
    {
        private static ILogger _Logger = Shared.Logger.LoggerFactory.CreateLogger("Logger");
        private static ILoggerFactory _Factory = null;

        public static void ConfigureLogger(ILoggerFactory factory)
        {
            try
            {
#pragma warning disable CS0618
                factory.AddConsole();
                factory.AddDebug(LogLevel.None);
                factory.AddFile("Logs/logger.log");
            }
            catch (Exception ex)
            {
                _Logger.LogError("Error in ConfigureLogger", ex);
            }
        }

        public static ILoggerFactory LoggerFactory
        {
            get
            {
                try
                {
                    if (_Factory == null)
                    {
                        _Factory = new LoggerFactory();
                        ConfigureLogger(_Factory);
                    }
                    return _Factory;
                }
                catch (Exception ex)
                {
                    _Logger.LogError("Error in get", ex);
                    return null;
                }
            }
            set
            {
                try
                {
                    _Factory = value;
                }
                catch (Exception ex)
                {
                    _Logger.LogError("Error in set", ex);
                }
            }
        }

        public static ILogger CreateLogger(string categoryName)
        {
            try
            {
                return LoggerFactory.CreateLogger(categoryName);
            }
            catch (Exception ex)
            {
                _Logger.LogError("Error in CreateLogger", ex);
                return null;
            }
        }
    }
}