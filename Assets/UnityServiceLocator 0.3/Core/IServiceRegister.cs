using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityServiceLocator
{
    public interface IServiceRegister
    {
        public TClass RegisterResources<TClass, TInterface>(string path) where TClass : Object, TInterface where TInterface : class;
        public TClass RegisterResources<TClass>(string path) where TClass : Object;
        public TClass RegisterResources<TClass>() where TClass : Object;
        public TClass RegisterInstantiate<TClass>(TClass instantiateMono) where TClass : Object;
        public TClass RegisterInstantiate<TClass, TInterface>(TClass instantiateMono) where TClass : Object, TInterface where TInterface : class;
        public TClass RegisteNewGameobject<TClass>() where TClass : MonoBehaviour;
        public TClass RegisteNewGameobject<TClass, TInterface>() where TClass : MonoBehaviour, TInterface where TInterface : class;
        public TClass RegisterNewClass<TClass>() where TClass : class, new();
        public TClass Register<TClass>(TClass registerClass) where TClass : class;
        public TClass Register<TClass, TInterface>(TClass registerMono) where TClass : Object, TInterface where TInterface : class;
    }
}