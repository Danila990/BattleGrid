using UnityEngine;

namespace UnityServiceLocator
{
    public abstract class ServiceContext : MonoBehaviour, IServiceContext
    {
        protected bool _isBuilded = false;

        public void BuildContext(IServiceRegister builder)
        {
            if (_isBuilded) return;

            _isBuilded = true;
            Configurate(builder);
        }

        protected abstract void Configurate(IServiceRegister builder);
    }
}
