using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;

namespace Policies
{
    public class RequiredClaims : IAuthorizationRequirement
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("RequiredClaims");

        public ClaimData[] Claims { get; set; }

        public RequiredClaims(ClaimData[] claims)
        {
            try
            {
                Claims = claims;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in RequiredClaims", ex);
            }
        }
    }
}