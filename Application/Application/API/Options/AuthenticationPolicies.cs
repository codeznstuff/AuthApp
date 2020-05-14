using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

namespace API.Options
{
    public static class AuthenticationPolicies
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("AuthenticationPolicies");

        public static JwtBearerOptions GetJwtBearerOptions(JwtBearerOptions options)
        {
            try
            {
                options.Authority = Shared.Config.GetConfigValue("AzureAd:Authority");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudiences = new List<string>
                        {
                            Shared.Config.GetConfigValue("AzureAd:ClientId"),
                            Shared.Config.GetConfigValue("AzureAd:Audience")
                        }
                };
                return options;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetJwtBearerOptions", ex);
                throw ex;
            }
        }
    }
}