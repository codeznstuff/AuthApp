using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Shared
{
    // This class encapsulates the details of getting a token from MSAL and exposes it via the
    // IAuthenticationProvider interface so that GraphServiceClient or AuthHandler can use it.
    // A significantly enhanced version of this class will in the future be available from
    // the GraphSDK team.  It will supports all the types of Client Application as defined by MSAL.
    public class MsalAuthenticationProvider : IAuthenticationProvider
    {
        private static readonly ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("MsalAuthenticationProvider");
        private readonly IConfidentialClientApplication _clientApplication;
        private readonly string[] _scopes;

        public MsalAuthenticationProvider(IConfidentialClientApplication clientApplication, string[] scopes)
        {
            try
            {
                _clientApplication = clientApplication;
                _scopes = scopes;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in MsalAuthenticationProvider", ex);
            }
        }

        /// <summary>
        /// Update HttpRequestMessage with credentials
        /// </summary>
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            try
            {
                var token = await GetTokenAsync();
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AuthenticateRequestAsync", ex);
            }
        }

        /// <summary>
        /// Acquire Token
        /// </summary>
        public async Task<string> GetTokenAsync()
        {
            try
            {
                AuthenticationResult authResult = null;
                authResult = await _clientApplication.AcquireTokenForClient(_scopes).ExecuteAsync();
                return authResult.AccessToken;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetTokenAsync", ex);
                return null;
            }
        }
    }
}