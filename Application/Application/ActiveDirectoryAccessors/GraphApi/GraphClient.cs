using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Shared;
using System;
using System.Collections.Generic;

namespace ActiveDirectoryAccessors.GraphApi
{
    internal class GraphApiClient
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("GraphApiClient");

        public GraphServiceClient GetGraphClient()
        {
            try
            {
                var authenticationProvider = CreateAuthorizationProvider();
                return new GraphServiceClient(authenticationProvider);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetGraphClient", ex);
                return null;
            }
        }

        private IAuthenticationProvider CreateAuthorizationProvider()
        {
            try
            {
                var clientId = Shared.Config.GetConfigValue("AzureAd:ClientId");
                var clientSecret = Shared.Config.GetConfigValue("AzureAd:ClientSecret");
                var redirectUri = Shared.Config.GetConfigValue("AzureAd:RedirectUri");
                var tenantId = Shared.Config.GetConfigValue("AzureAd:TenantId");
                var scope = Shared.Config.GetConfigValue("AzureAd:DefaultScope");

                List<string> scopes = new List<string>();
                scopes.Add(scope);

                var cca = ConfidentialClientApplicationBuilder.Create(clientId).WithTenantId(tenantId).WithRedirectUri(redirectUri).WithClientSecret(clientSecret).Build();
                return new MsalAuthenticationProvider(cca, scopes.ToArray());
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in CreateAuthorizationProvider", ex);
                return null;
            }
        }
    }
}