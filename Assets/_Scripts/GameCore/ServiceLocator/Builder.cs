using UnityEngine;
using Object = UnityEngine.Object;

namespace GameCore.UnityServiceLocator
{
    public interface IBuilder
    {
        public void RegisterInstantiate<T>(T mono) where T : Object;
        public void RegisterInstantiate<T, I>(T mono) where T : Object, I where I : class;
        public void RegisteNewGameobject<T>() where T : MonoBehaviour;
        public void RegisteNewGameobject<T, I>() where T : MonoBehaviour, I where I : class;
        public void Register<T>(T registerClass) where T : class;
        public void RegisterNewClass<T>() where T : class, new();
    }

    public class Builder : IBuilder
    {
        private IContainerSeter _register;

        public Builder(IContainerSeter register)
        {
            _register = register;
        }

        public void RegisterInstantiate<T>(T mono) where T : Object
        {
            T newMono = Object.Instantiate<T>(mono);
            _register.Set<T>(newMono);
        }

        public void RegisterInstantiate<T, I>(T mono) where T : Object, I where I : class
        {
            T newMono = Object.Instantiate<T>(mono);

            if (newMono is I)
                _register.Set<I>(newMono);
            else
                Debug.LogError($"{typeof(T).Name} does not implement interface {typeof(I).Name}");
        }

        public void RegisteNewGameobject<T>() where T : MonoBehaviour
        {
            T newMono = new GameObject(typeof(T).Name).AddComponent<T>();
            _register.Set<T>(newMono);
        }

        public void RegisteNewGameobject<T,I>() where T : MonoBehaviour, I where I : class
        {
            T newMono = new GameObject(typeof(T).Name).AddComponent<T>();

            if (newMono is I)
                _register.Set<I>(newMono);
            else
                Debug.LogError($"{typeof(T).Name} does not implement interface {typeof(I).Name}");
        }

        public void Register<T>(T register) where T : class
        {
            _register.Set<T>(register);
        }

        public void RegisterNewClass<T>() where T: class, new()
        {
            T newClass = new T();
            _register.Set<T>(newClass);
        }
    }
}