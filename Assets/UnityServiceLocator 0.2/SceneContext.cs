using UnityEngine;

namespace UnityServiceLocator
{
    [DefaultExecutionOrder(-999)]
    public abstract class SceneContext : ServiceContext
    {
        [SerializeField] private bool _isAutoBuild = true;
        [SerializeField] private bool _injectSceneMonoBehaviour = true;

        private IRootContoller _sceneRoot;

        private void Awake()
        {
            if (_isAutoBuild)
                BuildScope();
        }

        public void BuildScope()
        {
            if (_isBuilded) return;

            ServiceLocator.BuildScope(this, _injectSceneMonoBehaviour);
            if(_sceneRoot != null)
            {
                ServiceLocator.Inject(_sceneRoot);
                _sceneRoot.OnAwake();
            }
        }

        protected override abstract void Configurate(IServiceRegister builder);

        public void RegisterSceneRoot<TSceneRoot>() where TSceneRoot : MonoBehaviour, IRootContoller
        {
            _sceneRoot = new GameObject(typeof(TSceneRoot).Name).AddComponent<TSceneRoot>();
        }
    }
}
