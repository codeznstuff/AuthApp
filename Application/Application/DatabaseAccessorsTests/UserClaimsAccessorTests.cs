using DatabaseAccessors;
using DatabaseAccessors.DataTransferObjects;
using DatabaseAccessors.Interfaces;
using Mocks;
using System;
using System.Linq;
using Xunit;

namespace DatabaseAccessorsTests
{
    public class UserClaimsAccessorTests
    {
        [Fact]
        public void DeleteUserClaim_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var userClaimsAccessor = accessorFactory.CreateAccessor<IUserClaimsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var userClaimId = DatabaseMock.GetId("userClaimId");
            var userId = DatabaseMock.GetId("userId");

            // Act
            userClaimsAccessor.DeleteUserClaim(userClaimId);
            var result = userClaimsAccessor.FindAllUserClaims(userId).Where(userClaim => userClaim.ClaimId == userClaimId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void FindAllUserClaims_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var userClaimsAccessor = accessorFactory.CreateAccessor<IUserClaimsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var userId = DatabaseMock.GetId("userId");

            // Act
            var result = userClaimsAccessor.FindAllUserClaims(userId);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void SaveUserClaim_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var userClaimsAccessor = accessorFactory.CreateAccessor<IUserClaimsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var claimId = DatabaseMock.GetId("claimId");
            var userId = DatabaseMock.GetId("userId");
            var userClaim = new UserClaim
            {
                Id = Guid.NewGuid(),
                ClaimId = claimId,
                UserId = userId
            };

            // Act
            var result = userClaimsAccessor.SaveUserClaim(userClaim);

            // Assert
            Assert.NotNull(result);
        }
    }
}