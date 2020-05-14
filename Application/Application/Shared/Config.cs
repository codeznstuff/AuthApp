using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Shared
{
    public static class Config
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("Config");

        private static IConfigurationRoot Configuration()
        {
            try
            {
                var config = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true).AddEnvironmentVariables().Build();

                return config;
            }
            catch (FileNotFoundException ex)
            {
                Logger.LogError("Error in Configuration", ex);
                throw ex;
            }
        }

        public static string GetConfigValue(string environmentVariable)
        {
            try
            {
                var config = Configuration();

                var result = config[environmentVariable];

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetConfigValue", ex);
                return null;
            }
        }
    }
}