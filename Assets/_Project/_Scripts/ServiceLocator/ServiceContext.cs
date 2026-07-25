using UnityEngine;

namespace GameCore.UnityServiceLocator
{
    public abstract class ServiceContext : MonoBehaviour, IServiceContext
    {
        protected bool _isBuilded = false;

        public void BuildContext(IServiceBuilder builder)
        {
            if (_isBuilded) return;

            _isBuilded = true;
            Configurate(builder);
        }

        protected abstract void Configurate(IServiceBuilder builder);
    }
}
