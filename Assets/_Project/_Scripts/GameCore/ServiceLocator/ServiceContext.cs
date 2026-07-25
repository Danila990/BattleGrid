using UnityEngine;

namespace GameCore.UnityServiceLocator
{
    public abstract class ServiceContext : MonoBehaviour, IServiceContext
    {
        public abstract void Configurate(IBuilder builder);
    }
}
