using AuthorizationManager.Interfaces;
using AuthorizationManager.Managers;
using Microsoft.Extensions.Logging;
using Shared;
using System;

namespace AuthorizationManager
{
    public class ManagerFactory : FactoryBase
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("ManagerFactory");

        public ManagerFactory()
        {
            AddType<IApplicationManager>(typeof(ApplicationManager));
        }

        public T CreateManager<T>() where T : class
        {
            try
            {
                T result = GetInstanceForType<T>();
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in CreateManager", ex);
                return null;
            }
        }
    }
}