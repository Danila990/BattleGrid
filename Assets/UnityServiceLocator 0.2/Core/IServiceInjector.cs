using UnityEngine;

namespace UnityServiceLocator
{
    public interface IServiceInjector
    {
        public void InjectAllMonoBehaviour();
        public IServiceInjector Inject(object obj);
        public IServiceInjector InjectMono(object obj);
        public IServiceInjector InjectMono(MonoBehaviour obj);
    }
}