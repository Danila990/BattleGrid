using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityServiceLocator
{
    public class ServiceContainer : IServiceContainer
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public IEnumerable<object> Services => _services.Values;

        public void Clear()
        {
            _services.Clear();
        }

        public T Get<T>(Type type) where T : class
        {
            if (_services.TryGetValue(type, out object obj))
                return obj as T;

            Debug.LogError($"ServiceContainer get of type {type.FullName} - not registered");
            return null;
        }

        public T Get<T>() where T : class
        {
            return Get<T>(typeof(T));
        }

        public IServiceSeter Set<T>(T service) where T : class
        {
            Set<T>(service, typeof(T));
            return this;
        }

        public IServiceSeter Set<T>(T service, Type type) where T : class
        {
            if (!_services.TryAdd(type, service))
                Debug.LogError($"ServiceContainer set of type {type.FullName} - already registered");

            return this;
        }
    }
}