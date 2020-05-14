using AuthorizationManager;
using AuthorizationManager.Interfaces;
using Mocks;
using System;
using Xunit;

namespace AuthorizationManagerTests
{
    public class ApplicationManagerTests
    {
        [Fact]
        public void GetUser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var managerFactory = new ManagerFactory();
            var manager = managerFactory.CreateManager<IApplicationManager>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var userId = DatabaseMock.GetId("userId");

            // Act
            var result = manager.GetUser(userId);

            // Assert
            Assert.NotEmpty(result.Applications);
        }

        [Fact]
        public void GetApplicationUser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var managerFactory = new ManagerFactory();
            var manager = managerFactory.CreateManager<IApplicationManager>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            string applicationName = DatabaseMock.GetValue("applicationName");
            var userId = DatabaseMock.GetId("userId");

            // Act
            var result = manager.GetApplicationUser(applicationName, userId);

            // Assert
            Assert.NotEmpty(result.Claims);
        }

        [Fact]
        public void GetApplicationUsers_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var managerFactory = new ManagerFactory();
            var manager = managerFactory.CreateManager<IApplicationManager>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            string applicationName = DatabaseMock.GetValue("applicationName");

            // Act
            var result = manager.GetApplicationUsers(applicationName);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void RemoveApplication_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var managerFactory = new ManagerFactory();
            var manager = managerFactory.CreateManager<IApplicationManager>();
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var applicationId = DatabaseMock.GetId("applicationId");
            string applicationName = DatabaseMock.GetValue("applicationName");

            // Act
            manager.RemoveApplication(applicationId);

            // Assert
            Assert.Null(manager.GetApplicationUsers(applicationName));
        }

        [Fact]
        public void AddApplication_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var managerFactory = new ManagerFactory();
            var manager = managerFactory.CreateManager<IApplicationManager>();
            string applicationName = Guid.NewGuid().ToString();

            // Act
            var result = manager.AddApplication(applicationName);

            // Assert
            Assert.NotNull(result);
        }
    }
}