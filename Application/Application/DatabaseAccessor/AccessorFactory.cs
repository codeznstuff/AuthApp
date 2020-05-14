using DatabaseAccessors.Accessors;
using DatabaseAccessors.Interfaces;
using Microsoft.Extensions.Logging;
using Shared;
using System;

namespace DatabaseAccessors
{
    public class AccessorFactory : FactoryBase
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("AccessorFactory");

        public AccessorFactory()
        {
            AddType<IApplicationsAccessor>(typeof(ApplicationsAccessor));
            AddType<IClaimsAccessor>(typeof(ClaimsAccessor));
            AddType<IMembershipsAccessor>(typeof(MembershipsAccessor));
            AddType<IUserClaimsAccessor>(typeof(UserClaimsAccessor));
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