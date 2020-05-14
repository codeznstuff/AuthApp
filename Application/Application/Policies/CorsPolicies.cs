using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Logging;
using System;

namespace Policies
{
    public static class CorsPolicies
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("CorsPolicies");

        public static CorsOptions GetCorsOptions(CorsOptions options)
        {
            try
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
                return options;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetCorsOptions", ex);
                throw ex;
            }
        }
    }
}