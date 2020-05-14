using ActiveDirectoryAccessors.Accessors;
using ActiveDirectoryAccessors.Interfaces;
using Microsoft.Extensions.Logging;
using Shared;
using System;

namespace ActiveDirectoryAccessors
{
    public class AccessorFactory : FactoryBase
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("AccessorFactory");

        public AccessorFactory()
        {
            AddType<IGraphAccessor>(typeof(GraphAccessor));
        }

        public T CreateAccessor<T>() where T : class
        {
            try
            {
                T result = GetInstanceForType<T>();
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in CreateAccessor", ex);
                return null;
            }
        }
    }
}