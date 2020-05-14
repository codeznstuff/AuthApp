using DatabaseAccessors;
using DatabaseAccessors.DataTransferObjects;
using DatabaseAccessors.Interfaces;
using Mocks;
using System;
using Xunit;

namespace DatabaseAccessorsTests
{
    public class ClaimsAccessorTests
    {
        [Fact]
        public void DeleteClaim_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var claimsAccessor = accessorFactory.CreateAccessor<IClaimsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var claimId = DatabaseMock.GetId("claimId");

            // Act
            claimsAccessor.DeleteClaim(claimId);

            // Assert
            Assert.Null(claimsAccessor.Find(claimId));
        }

        [Fact]
        public void Find_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var claimsAccessor = accessorFactory.CreateAccessor<IClaimsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var claimId = DatabaseMock.GetId("claimId");

            // Act
            var result = claimsAccessor.Find(claimId);

            // Assert
            Assert.Equal(expected: claimId, actual: result.Id);
        }

        [Fact]
        public void SaveClaim_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var claimsAccessor = accessorFactory.CreateAccessor<IClaimsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var id = DatabaseMock.GetId("Applications");
            var claim = new Claim
            {
                Id = Guid.NewGuid(),
                ApplicationId = id,
                ClaimType = Guid.NewGuid().ToString(),
                ClaimValue = Guid.NewGuid().ToString()
            };

            // Act
            var result = claimsAccessor.SaveClaim(claim);

            // Assert
            Assert.NotNull(result);
        }
    }
}