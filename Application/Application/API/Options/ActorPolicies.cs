using Akka.Actor;
using Akka.Routing;
using API.Actors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace API.Options
{
    public class ActorPolicies
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("ActorPolicies");

        public static ActorSystem GetActorSystemOptions(IServiceProvider options)
        {
            try
            {
                return ActorSystem.Create(Shared.Config.GetConfigValue("ActorSystem:Name"));
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetActorSystemOptions", ex);
                throw ex;
            }
        }

        public static IActorRef GetIActorRefOptions(IServiceProvider options)
        {
            try
            {
                ActorSystem actorSystem = options.GetRequiredService<ActorSystem>();

                Props actorRouter = Props.Create<ApplicationsActor>().WithRouter(new RoundRobinPool(Convert.ToInt32(Shared.Config.GetConfigValue("ActorSystem:PoolInstances"))));

                IActorRef actorPool = actorSystem.ActorOf(actorRouter, Shared.Config.GetConfigValue("ActorSystem:PoolName"));

                return actorPool;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetIActorRefOptions", ex);
                throw ex;
            }
        }
    }
}