using DatabaseAccessors;
using DatabaseAccessors.DataTransferObjects;
using DatabaseAccessors.Interfaces;
using Mocks;
using System;
using System.Linq;
using Xunit;

namespace DatabaseAccessorsTests
{
    public class MembershipsAccessorTests
    {
        [Fact]
        public void DeleteMembership_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var membershipsAccessor = accessorFactory.CreateAccessor<IMembershipsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var membershipId = DatabaseMock.GetId("membershipId");
            var userId = DatabaseMock.GetId("userId");

            // Act
            membershipsAccessor.DeleteMembership(membershipId);
            var result = membershipsAccessor.FindAllUserApplications(userId).Where(membership => membership.Id == membershipId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void FindAllUserApplications_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var membershipsAccessor = accessorFactory.CreateAccessor<IMembershipsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var userId = DatabaseMock.GetId("userId");

            // Act
            var result = membershipsAccessor.FindAllUserApplications(userId);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void SaveMembership_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var membershipsAccessor = accessorFactory.CreateAccessor<IMembershipsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var applicationId = DatabaseMock.GetId("applicationId");
            var userId = DatabaseMock.GetId("userId");
            var membership = new Membership
            {
                Id = Guid.NewGuid(),
                ApplicationId = applicationId,
                UserId = userId
            };

            // Act
            var result = membershipsAccessor.SaveMembership(membership);

            // Assert
            Assert.NotNull(result);
        }
    }
}