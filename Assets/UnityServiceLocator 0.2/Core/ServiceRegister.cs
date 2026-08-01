using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityServiceLocator
{
    public class ServiceRegister : IServiceRegister
    {
        private IServiceSeter _container;

        public ServiceRegister(IServiceSeter container)
        {
            _container = container;
        }

        public virtual TClass RegisterResources<TClass, TInterface>(string path) where TClass : Object, TInterface where TInterface : class
        {
            TClass loadResources = Resources.Load<TClass>(path);
            loadResources = Object.Instantiate<TClass>(loadResources);
            return Register<TClass, TInterface>(loadResources);
        }

        public virtual TClass RegisterResources<TClass>(string path) where TClass : Object
        {
            TClass loadResources = Resources.Load<TClass>(path);
            loadResources = Object.Instantiate<TClass>(loadResources);
            return Register<TClass>(loadResources); 
        }

        public virtual TClass RegisterInstantiate<TClass>(TClass instantiateMono) where TClass : Object
        {
            TClass newMono = Object.Instantiate<TClass>(instantiateMono);
            return Register<TClass>(newMono);
        }

        public virtual TClass RegisterInstantiate<TClass, TInterface>(TClass instantiateMono) where TClass : Object, TInterface where TInterface : class
        {
            TClass newMono = Object.Instantiate<TClass>(instantiateMono);
            return Register<TClass, TInterface>(newMono);
        }

        public virtual TClass RegisteNewGameobject<TClass>() where TClass : MonoBehaviour
        {
            TClass newMono = new GameObject(typeof(TClass).Name).AddComponent<TClass>();
            return Register<TClass>(newMono);
        }

        public virtual TClass RegisteNewGameobject<TClass,TInterface>() where TClass : MonoBehaviour, TInterface where TInterface : class
        {
            TClass newMono = new GameObject(typeof(TClass).Name).AddComponent<TClass>();
            return Register<TClass, TInterface>(newMono);
        }

        public TClass RegisterNewClass<TClass>() where TClass: class, new()
        {
            TClass newClass = new TClass();
            return Register<TClass>(newClass);
        }

        public TClass Register<TClass,TInterface>(TClass registerMono) where TClass : Object, TInterface where TInterface : class
        {
            if (registerMono is TInterface registerInterface)
                _container.Set<TInterface>(registerInterface);
            else
                Debug.LogError($"{typeof(TClass).Name} does not implement interface {typeof(TInterface).Name}");

            return registerMono;
        }

        public TClass Register<TClass>(TClass registerClass) where TClass : class
        {
            _container.Set<TClass>(registerClass);
            return registerClass;
        }
    }
}