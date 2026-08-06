
using System.Collections;
using UnityEngine;

namespace UnityServiceLocator
{
    [DefaultExecutionOrder(-999)]
    public abstract class SceneContext : ServiceContext
    {
        [SerializeField] private bool _isAutoBuild = true;

        private IEntryPoint _root;

        private void Awake()
        {
            if (_isAutoBuild)
                BuildScope();
        }

        public void BuildScope()
        {
            if (_isBuilded) return;

            ServiceLocator.BuildScope(this);
            if(_root != null)
            {
                ServiceLocator.Inject(_root);
                _root.GameInit();
            }
        }

        protected override abstract void Configurate(IServiceRegister builder);

        public void RegisterRoot<TRoot>() where TRoot : MonoBehaviour, IEntryPoint
        {
            _root = new GameObject(nameof(TRoot)).AddComponent<TRoot>();
        }
    }
}
