using AuthorizationManager.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Policies
{
    public class RequiredClaimsHandler : AuthorizationHandler<RequiredClaims>
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("RequiredClaimsHandler");

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequiredClaims requirement)
        {
            try
            {
                var userId = context.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier");
                var authorizationManager = new ApplicationManager();
                var user = authorizationManager.GetUser(new System.Guid(userId.Value));
                var applicationName = Shared.Config.GetConfigValue("Name");
                var claims = user.Applications.ToList().Where(app => app.ApplicationName == applicationName).Select(claim => claim.Claims).FirstOrDefault();

                bool hasAll = requirement.Claims.All(requirementClaim => claims.Any(claim => claim.ClaimType == requirementClaim.ClaimType && claim.ClaimValue == requirementClaim.ClaimValue));
                if (hasAll)
                {
                    context.Succeed(requirement);
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in HandleRequirementAsync", ex);
                throw ex;
            }
        }
    }
}