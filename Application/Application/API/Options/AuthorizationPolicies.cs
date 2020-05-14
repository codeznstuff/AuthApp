using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace API.Options
{
    public static class AuthorizationPolicies
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("AuthorizationPolicies");

        public static AuthorizationOptions GetAuthorizationOptions(AuthorizationOptions options)
        {
            try
            {
                var requiresAdministrativePrivilegesClaims = new List<ClaimData>();
                var claim = new ClaimData
                {
                    ClaimType = "Role",
                    ClaimValue = "Admin"
                };
                requiresAdministrativePrivilegesClaims.Add(claim);
                options.AddPolicy("RequiresAdministrativePrivileges", policy => policy.Requirements.Add(new RequiredClaims(requiresAdministrativePrivilegesClaims.ToArray())));
                return options;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetAuthorizationOptions", ex);
                throw ex;
            }
        }
    }
}