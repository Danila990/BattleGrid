using UnityEngine;

namespace UnityServiceLocator
{
    public interface IServiceInjector
    {
        public void InjectAllScene();
        public IServiceInjector Inject(object obj);
    }
}