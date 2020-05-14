using DatabaseAccessors;
using DatabaseAccessors.DataTransferObjects;
using DatabaseAccessors.Interfaces;
using Mocks;
using System;
using Xunit;

namespace DatabaseAccessorsTests
{
    public class ApplicationsAccessorTests
    {
        [Fact]
        public void DeleteApplication_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var applicationsAccessor = accessorFactory.CreateAccessor<IApplicationsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var applicationId = DatabaseMock.GetId("applicationId");

            // Act
            applicationsAccessor.DeleteApplication(applicationId);

            // Assert
            Assert.Null(applicationsAccessor.Find(applicationId));
        }

        [Fact]
        public void Find_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var applicationsAccessor = accessorFactory.CreateAccessor<IApplicationsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var applicationId = DatabaseMock.GetId("applicationId");

            // Act
            var result = applicationsAccessor.Find(applicationId);

            // Assert
            Assert.Equal(expected: applicationId, actual: result.Id);
        }

        [Fact]
        public void FindAllApplications_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var applicationsAccessor = accessorFactory.CreateAccessor<IApplicationsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();

            // Act
            var result = applicationsAccessor.FindAllApplications();

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void FindAllUsersForApplication_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var applicationsAccessor = accessorFactory.CreateAccessor<IApplicationsAccessor>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            string applicationName = DatabaseMock.GetValue("applicationName");

            // Act
            var result = applicationsAccessor.FindAllUsersForApplication(applicationName);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void SaveApplication_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var applicationsAccessor = accessorFactory.CreateAccessor<IApplicationsAccessor>();
            var application = new Application()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString()
            };

            // Act
            var result = applicationsAccessor.SaveApplication(application);

            // Assert
            Assert.NotNull(result);
        }
    }
}