using DatabaseAccessors;
using DatabaseAccessors.DataTransferObjects;
using DatabaseAccessors.Interfaces;
using System;

namespace Mocks
{
    public class DatabaseMock
    {
        private static Guid userId = new Guid("be64b27b-59b6-43bb-a824-51c16d328ffb");

        private static Application application = new Application
        {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString()
        };

        private static Claim claim = new Claim
        {
            Id = Guid.NewGuid(),
            ApplicationId = application.Id,
            ClaimType = Guid.NewGuid().ToString(),
            ClaimValue = Guid.NewGuid().ToString()
        };

        private static Membership membership = new Membership
        {
            Id = Guid.NewGuid(),
            ApplicationId = application.Id,
            UserId = userId
        };

        private static UserClaim userClaim = new UserClaim
        {
            Id = Guid.NewGuid(),
            ClaimId = claim.Id,
            UserId = userId
        };

        public void SeedDatabase()
        {
            var accessorFactory = new AccessorFactory();
            var applicationsAccessor = accessorFactory.CreateAccessor<IApplicationsAccessor>();
            var claimsAccessor = accessorFactory.CreateAccessor<IClaimsAccessor>();
            var userClaimsAccessor = accessorFactory.CreateAccessor<IUserClaimsAccessor>();
            var membershipsAccessor = accessorFactory.CreateAccessor<IMembershipsAccessor>();

            applicationsAccessor.SaveApplication(application);
            claimsAccessor.SaveClaim(claim);
            userClaimsAccessor.SaveUserClaim(userClaim);
            membershipsAccessor.SaveMembership(membership);
        }

        public Guid GetId(string property)
        {
            switch (property)
            {
                case "applicationId":
                    return application.Id;

                case "claimId":
                    return claim.Id;

                case "userClaimId":
                    return userClaim.Id;

                case "membersipId":
                    return membership.Id;

                case "userId":
                    return userId;

                default:
                    return new Guid();
            }
        }

        public string GetValue(string property)
        {
            switch (property)
            {
                case "applicationName":
                    return application.Name;

                case "claimType":
                    return claim.ClaimType;

                case "claimValue":
                    return claim.ClaimValue;

                default:
                    return String.Empty;
            }
        }
    }
}