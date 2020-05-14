using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Shared
{
    public class FactoryBase
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("FactoryBase");

        // contains the dictionary of overrides provided for this factory instance
        private readonly Dictionary<string, object> _overrides = new Dictionary<string, object>();

        // contains the dictionary of types supported by this factory
        private readonly Dictionary<string, Type> _types = new Dictionary<string, Type>();

        /// <summary>
        /// Provides mock override objects for testing purposes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void AddOverride<T>(T obj)
        {
            try
            {
                if (_overrides.ContainsKey(typeof(T).Name))
                {
                    _overrides.Remove((typeof(T).Name));
                }
                _overrides.Add(typeof(T).Name, obj);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AddOverride", ex);
            }
        }

        /// <summary>
        /// Configure the types supported by this factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void AddType<T>(Type obj)
        {
            try
            {
                if (_types.ContainsKey(typeof(T).Name))
                {
                    _types.Remove((typeof(T).Name));
                }
                _types.Add(typeof(T).Name, obj);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AddType", ex);
            }
        }

        protected T GetInstanceForType<T>() where T : class
        {
            try
            {
                // Return the override, if one exists for the type T
                if (_overrides.ContainsKey(typeof(T).Name))
                {
                    return _overrides[typeof(T).Name] as T;
                }

                // No override, so return an instance of the type from the configured types
                if (_types.ContainsKey(typeof(T).Name))
                {
                    var type = _types[typeof(T).Name] as Type;
                    if (type != null)
                    {
                        return Activator.CreateInstance(type) as T;
                    }
                }

                // Oops, no override OR configuration for this type
                throw new ArgumentException($"{typeof(T).Name} is not supported by this factory");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetInstanceForType", ex);
                throw ex;
            }
        }
    }
}