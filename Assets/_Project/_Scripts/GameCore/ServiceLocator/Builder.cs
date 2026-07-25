using UnityEngine;
using Object = UnityEngine.Object;

namespace GameCore.UnityServiceLocator
{
    public interface IBuilder
    {
        public TClass RegisterInstantiate<TClass>(TClass instantiateMono) where TClass : Object;
        public TClass RegisterInstantiate<TClass, IInterface>(TClass instantiateMono) where TClass : Object, IInterface where IInterface : class;
        public TClass RegisteNewGameobject<TClass>() where TClass : MonoBehaviour;
        public TClass RegisteNewGameobject<TClass, IInterface>() where TClass : MonoBehaviour, IInterface where IInterface : class;
        public void Register<TClass>(TClass registerClass) where TClass : class;
        public TClass RegisterNewClass<TClass>() where TClass : class, new();
    }

    public class Builder : IBuilder
    {
        private IContainerSeter _container;

        public Builder(IContainerSeter container)
        {
            _container = container;
        }

        public TClass RegisterResources<TClass>(string path) where TClass : Object
        {
            TClass loadResources = null;
            return null;
        }

        public TClass RegisterInstantiate<TClass>(TClass instantiateMono) where TClass : Object
        {
            TClass newMono = Object.Instantiate<TClass>(instantiateMono);
            _container.Set<TClass>(newMono);
            return newMono;
        }

        public TClass RegisterInstantiate<TClass, IInterface>(TClass instantiateMono) where TClass : Object, IInterface where IInterface : class
        {
            TClass newMono = Object.Instantiate<TClass>(instantiateMono);

            if (newMono is IInterface)
                _container.Set<IInterface>(newMono);
            else
                Debug.LogError($"{typeof(TClass).Name} does not implement interface {typeof(IInterface).Name}");

            return newMono;
        }

        public TClass RegisteNewGameobject<TClass>() where TClass : MonoBehaviour
        {
            TClass newMono = new GameObject(typeof(TClass).Name).AddComponent<TClass>();
            _container.Set<TClass>(newMono);
            return newMono;
        }

        public TClass RegisteNewGameobject<TClass,IInterface>() where TClass : MonoBehaviour, IInterface where IInterface : class
        {
            TClass newMono = new GameObject(typeof(TClass).Name).AddComponent<TClass>();

            if (newMono is IInterface)
                _container.Set<IInterface>(newMono);
            else
                Debug.LogError($"{typeof(TClass).Name} does not implement interface {typeof(IInterface).Name}");

            return newMono;
        }

        public TClass RegisterNewClass<TClass>() where TClass: class, new()
        {
            TClass newClass = new TClass();
            _container.Set<TClass>(newClass);
            return newClass;
        }

        public void Register<TClass>(TClass registerClass) where TClass : class => _container.Set<TClass>(registerClass);
    }
}