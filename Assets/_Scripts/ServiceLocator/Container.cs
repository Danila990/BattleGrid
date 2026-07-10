using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityScope
{
    public interface IContainer : IContainerGeter, IContainerSeter
    {
    }

    public interface IContainerGeter
    {
        public IEnumerable<object> Services { get; }
        public IContainerGeter Get<T>(out T service) where T : class;
        public T Get<T>(Type type) where T : class;
        public T Get<T>() where T : class;
    }

    public interface IContainerSeter
    {
        public IContainerSeter Set<T>(T service) where T : class;
        public IContainerSeter Set<T>(T service, Type type) where T : class;
    }

    public class Container : IContainer
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public IEnumerable<object> Services => _services.Values;

        public IContainerGeter Get<T>(out T service) where T : class
        {
            service = Get<T>();
            return this;
        }

        public T Get<T>(Type type) where T : class
        {
            if (_services.TryGetValue(type, out object obj))
                return obj as T;

            Debug.LogError($"Container get of type {type.FullName} - not registered");
            return null;
        }

        public T Get<T>() where T : class
        {
            return Get<T>(typeof(T));
        }

        public IContainerSeter Set<T>(T service) where T : class
        {
            Set<T>(service, typeof(T));
            return this;
        }

        public IContainerSeter Set<T>(T service, Type type) where T : class
        {
            if (!_services.TryAdd(type, service))
                Debug.LogError($"Container set of type {type.FullName} - already registered");

            return this;
        }
    }
}