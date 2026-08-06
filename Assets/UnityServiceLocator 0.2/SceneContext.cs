using UnityEngine;

namespace UnityServiceLocator
{
    [DefaultExecutionOrder(-999)]
    public abstract class SceneContext : ServiceContext
    {
        [SerializeField] private bool _isAutoBuild = true;
        [SerializeField] private bool _injectSceneMonoBehaviour = true;

        private IEntryPoint _entryPoint;

        private void Awake()
        {
            if (_isAutoBuild)
                BuildScope();
        }

        public void BuildScope()
        {
            if (_isBuilded) return;

            ServiceLocator.BuildScope(this, _injectSceneMonoBehaviour);
            if(_entryPoint != null)
            {
                ServiceLocator.Inject(_entryPoint);
                _entryPoint.GameInit();
            }
        }

        protected override abstract void Configurate(IServiceRegister builder);

        public void RegisterEntryPoint<TEntryPoint>() where TEntryPoint : MonoBehaviour, IEntryPoint
        {
            _entryPoint = new GameObject(nameof(TEntryPoint)).AddComponent<TEntryPoint>();
        }
    }
}
