using Contracts;
using System;

namespace AuthorizationManager.Interfaces
{
    public interface IApplicationManager
    {
        User GetUser(Guid userId);

        ApplicationUser GetApplicationUser(string applicationName, Guid userId);

        ApplicationUser[] GetApplicationUsers(string applicationName);

        void RemoveApplication(Guid applicationId);

        Application AddApplication(string applicationName);

        void AddApplicationClaims(Guid applicationId, ClaimData claim);

        void RemoveApplicationClaims(Guid applicationId, Guid claimId);

        void AddUserToApplication(Guid applicationId, Guid userId);

        void RemoveUserFromApplication(Guid applicationId, Guid userId);

        void AddApplicationClaimForUser(Guid applicationId, Guid userId, ClaimData claim);

        void RemoveApplicationClaimForUser(Guid applicationId, Guid userId, Guid claimId);

        void RemoveUser(Guid userId);
    }
}