using UnityEngine;
using Object = UnityEngine.Object;

namespace GameCore.UnityServiceLocator
{
    public interface IServiceBuilder
    {
        public TClass RegisterResources<TClass, TInterface>(string path) where TClass : Object, TInterface where TInterface : class;
        public TClass RegisterResources<TClass>(string path) where TClass : Object;
        public TClass RegisterInstantiate<TClass>(TClass instantiateMono) where TClass : Object;
        public TClass RegisterInstantiate<TClass, TInterface>(TClass instantiateMono) where TClass : Object, TInterface where TInterface : class;
        public TClass RegisteNewGameobject<TClass>() where TClass : MonoBehaviour;
        public TClass RegisteNewGameobject<TClass, TInterface>() where TClass : MonoBehaviour, TInterface where TInterface : class;
        public TClass RegisterNewClass<TClass>() where TClass : class, new();
        public TClass Register<TClass>(TClass registerClass) where TClass : class;
        public TClass Register<TClass, TInterface>(TClass registerMono) where TClass : Object, TInterface where TInterface : class;
    }

    public class ServiceBuilder : IServiceBuilder
    {
        private IServiceSeter _container;

        public ServiceBuilder(IServiceSeter container)
        {
            _container = container;
        }

        public TClass RegisterResources<TClass, TInterface>(string path) where TClass : Object, TInterface where TInterface : class
        {
            TClass loadResources = Resources.Load<TClass>(path);
            loadResources = Object.Instantiate<TClass>(loadResources);
            return Register<TClass, TInterface>(loadResources);
        }

        public TClass RegisterResources<TClass>(string path) where TClass : Object
        {
            TClass loadResources = Resources.Load<TClass>(path);
            loadResources = Object.Instantiate<TClass>(loadResources);
            return Register<TClass>(loadResources); 
        }

        public TClass RegisterInstantiate<TClass>(TClass instantiateMono) where TClass : Object
        {
            TClass newMono = Object.Instantiate<TClass>(instantiateMono);
            return Register<TClass>(newMono);
        }

        public TClass RegisterInstantiate<TClass, TInterface>(TClass instantiateMono) where TClass : Object, TInterface where TInterface : class
        {
            TClass newMono = Object.Instantiate<TClass>(instantiateMono);
            return Register<TClass, TInterface>(newMono);
        }

        public TClass RegisteNewGameobject<TClass>() where TClass : MonoBehaviour
        {
            TClass newMono = new GameObject(typeof(TClass).Name).AddComponent<TClass>();
            return Register<TClass>(newMono);
        }

        public TClass RegisteNewGameobject<TClass,TInterface>() where TClass : MonoBehaviour, TInterface where TInterface : class
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
            if (registerMono is TInterface mono)
                _container.Set<TInterface>(mono);
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