using Akka.Actor;
using Akka.Routing;
using API.Actors;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Mocks;
using Xunit;

namespace ApiControllerTests
{
    public class ApplicationsControllerTests
    {
        [Fact]
        public void GetUser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            ActorSystem actorSystem = ActorSystem.Create("TestActorSystem");
            Props actorRouter = Props.Create<ApplicationsActor>().WithRouter(new RoundRobinPool(5));
            IActorRef actorPool = actorSystem.ActorOf(actorRouter, "Test");
            var applicationsController = new ApplicationsController(actorPool);
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var userId = DatabaseMock.GetId("userId");

            // Act
            var result = applicationsController.GetUser(userId).Result;

            // Assert
            Assert.NotNull(result.Value.Applications);
        }

        [Fact]
        public void GetApplicationUser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            ActorSystem actorSystem = ActorSystem.Create("TestActorSystem");
            Props actorRouter = Props.Create<ApplicationsActor>().WithRouter(new RoundRobinPool(5));
            IActorRef actorPool = actorSystem.ActorOf(actorRouter, "Test");
            var applicationsController = new ApplicationsController(actorPool);
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            string applicationName = DatabaseMock.GetValue("applicationName");
            var userId = DatabaseMock.GetId("userId");

            // Act
            var result = applicationsController.GetApplicationUser(applicationName, userId).Result;

            // Assert
            Assert.NotNull(result.Value.Claims);
        }

        [Fact]
        public void GetApplicationUsers_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            ActorSystem actorSystem = ActorSystem.Create("TestActorSystem");
            Props actorRouter = Props.Create<ApplicationsActor>().WithRouter(new RoundRobinPool(5));
            IActorRef actorPool = actorSystem.ActorOf(actorRouter, "Test");
            var applicationsController = new ApplicationsController(actorPool);
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            string applicationName = DatabaseMock.GetValue("applicationName");

            // Act
            var result = applicationsController.GetApplicationUsers(applicationName).Result;

            // Assert
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void AddApplication_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            ActorSystem actorSystem = ActorSystem.Create("TestActorSystem");
            Props actorRouter = Props.Create<ApplicationsActor>().WithRouter(new RoundRobinPool(5));
            IActorRef actorPool = actorSystem.ActorOf(actorRouter, "Test");
            var applicationsController = new ApplicationsController(actorPool);
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            string applicationName = DatabaseMock.GetValue("applicationName");

            // Act
            var result = applicationsController.AddApplication(applicationName).Result;

            // Assert
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void RemoveApplication_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            ActorSystem actorSystem = ActorSystem.Create("TestActorSystem");
            Props actorRouter = Props.Create<ApplicationsActor>().WithRouter(new RoundRobinPool(5));
            IActorRef actorPool = actorSystem.ActorOf(actorRouter, "Test");
            var applicationsController = new ApplicationsController(actorPool);
            var DatabaseMock = new DatabaseMock();
            DatabaseMock.SeedDatabase();
            var applicationId = DatabaseMock.GetId("applicationId");

            // Act
            var result = applicationsController.RemoveApplication(applicationId).Result;

            // Assert
            Assert.IsType<AcceptedResult>(result) ;
        }
    }
}