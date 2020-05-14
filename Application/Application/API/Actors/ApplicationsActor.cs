using Akka;
using Akka.Actor;
using AuthorizationManager;
using AuthorizationManager.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace API.Actors
{
    public class ApplicationsActor : UntypedActor
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("ApplicationsActor");

        public ApplicationsActor()
        {
        }

        protected override void OnReceive(object message)
        {
            try
            {
                message.Match().With<Messages.GetUser>(msg => HandleGetUserMessage(msg.UserId));
                message.Match().With<Messages.GetApplicationUser>(msg => HandleGetApplicationUserMessage(msg.ApplicationName, msg.UserId));
                message.Match().With<Messages.GetApplicationUsers>(msg => HandleGetApplicationUsersMessage(msg.ApplicationName));
                message.Match().With<Messages.AddApplication>(msg => HandleAddApplication(msg.ApplicationName));
                message.Match().With<Messages.RemoveApplication>(msg => HandleRemoveApplication(msg.ApplicationId));
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in OnReceive", ex);
            }
        }

        private void HandleGetUserMessage(Guid userId)
        {
            try
            {
                var managerFactory = new ManagerFactory();
                var applicationManager = managerFactory.CreateManager<IApplicationManager>();
                var user = applicationManager.GetUser(userId);
                Sender.Tell(user, Self);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in HandleGetUserMessage", ex);
                Sender.Tell(ex, Self);
            }
        }

        private void HandleGetApplicationUserMessage(string applicationName, Guid userId)
        {
            try
            {
                var managerFactory = new ManagerFactory();
                var applicationManager = managerFactory.CreateManager<IApplicationManager>();
                var applicationUser = applicationManager.GetApplicationUser(applicationName, userId);
                Sender.Tell(applicationUser, Self);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in HandleGetApplicationUserMessage", ex);
                Sender.Tell(ex, Self);
            }
        }

        private void HandleGetApplicationUsersMessage(string applicationName)
        {
            try
            {
                var managerFactory = new ManagerFactory();
                var applicationManager = managerFactory.CreateManager<IApplicationManager>();
                var applicationUsers = applicationManager.GetApplicationUsers(applicationName);
                Sender.Tell(applicationUsers, Self);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in HandleGetApplicationUsersMessage", ex);
                Sender.Tell(ex, Self);
            }
        }

        private void HandleAddApplication(string applicationName)
        {
            try
            {
                var managerFactory = new ManagerFactory();
                var applicationManager = managerFactory.CreateManager<IApplicationManager>();
                var application = applicationManager.AddApplication(applicationName);
                Sender.Tell(application, Self);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in HandleAddApplication", ex);
                Sender.Tell(ex, Self);
            }
        }

        private void HandleRemoveApplication(Guid applicationId)
        {
            try
            {
                var managerFactory = new ManagerFactory();
                var applicationManager = managerFactory.CreateManager<IApplicationManager>();
                applicationManager.RemoveApplication(applicationId);
                Sender.Tell(true, Self);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in HandleAddApplication", ex);
                Sender.Tell(ex, Self);
            }
        }
    }
}