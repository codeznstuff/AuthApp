using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;

namespace API.Options
{
    public class SwaggerPolicies
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("SwaggerPolicies");

        private static string applicationName = Shared.Config.GetConfigValue("Name");

        public static SwaggerGenOptions GetSwaggerGenOptions(SwaggerGenOptions options)
        {
            try
            {
                options.SwaggerDoc("v1", new Info { Title = applicationName, Version = "v1" });

                // Swagger 2.+ support
                //var security = new Dictionary<string, IEnumerable<string>>
                //    {
                //        {"Bearer", new string[] { }},
                //    };

                //options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\". Please enter into field the word 'Bearer' followed by a space and your JWT.",
                //    Name = "Authorization",
                //    In = "header",
                //    Type = "apiKey"
                //});

                //options.AddSecurityRequirement(security);
                return options;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetSwaggerGenOptions", ex);
                throw ex;
            }
        }

        public static SwaggerUIOptions GetSwaggerUIOptions(SwaggerUIOptions options)
        {
            try
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", applicationName);
                options.DocumentTitle = applicationName;
                options.DocExpansion(DocExpansion.None);
                options.RoutePrefix = string.Empty;
                return options;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetSwaggerUIOptions", ex);
                throw ex;
            }
        }
    }
}