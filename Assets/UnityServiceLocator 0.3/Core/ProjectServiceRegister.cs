using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityServiceLocator
{
    public class ProjectServiceRegister : ServiceRegister
    {
        public ProjectServiceRegister(IServiceSeter container) : base(container)
        {
        }

        public override TClass RegisteNewGameobject<TClass>()
        {
            return DontDestroy<TClass>(base.RegisteNewGameobject<TClass>());
        }

        public override TClass RegisteNewGameobject<TClass, TInterface>()
        {
            return DontDestroy<TClass>(base.RegisteNewGameobject<TClass, TInterface>());
        }

        public override TClass RegisterInstantiate<TClass, TInterface>(TClass instantiateMono)
        {
            return DontDestroy<TClass>(base.RegisterInstantiate<TClass, TInterface>(instantiateMono));
        }

        public override TClass RegisterInstantiate<TClass>(TClass instantiateMono)
        {
            return DontDestroy<TClass>(base.RegisterInstantiate<TClass>(instantiateMono));
        }

        public override TClass RegisterResources<TClass, TInterface>(string path)
        {
            return DontDestroy<TClass>(base.RegisterResources<TClass, TInterface>(path));
        }

        public override TClass RegisterResources<TClass>(string path)
        {
            return DontDestroy<TClass>(base.RegisterResources<TClass>(path));
        }

        private TClass DontDestroy<TClass>(TClass dontDestoyClass) where TClass : class
        {
            if(dontDestoyClass is MonoBehaviour registerMono)
                Object.DontDestroyOnLoad(registerMono);

            return dontDestoyClass;
        }
    }
}