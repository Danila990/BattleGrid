using System;
using System.Collections.Generic;

namespace UnityServiceLocator
{
    public interface IServiceContainer : IServiceGeter, IServiceSeter
    {
        public IEnumerable<object> Services { get; }
        public void Clear();
    }

    public interface IServiceGeter
    {
        public T Get<T>(Type type) where T : class;
        public T Get<T>() where T : class;
    }

    public interface IServiceSeter
    {
        public IServiceSeter Set<T>(T service) where T : class;
        public IServiceSeter Set<T>(T service, Type type) where T : class;
    }

}