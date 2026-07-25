using UnityEngine;

namespace GameCore.UnityServiceLocator
{
    [DefaultExecutionOrder(-999)]
    public abstract class EntryPointScope : MonoBehaviour, IServiceContext
    {
        [SerializeField] private bool _isAutoBuild = true;

        private bool _isBuilded = false;
        
        private void Awake()
        {
            if (_isAutoBuild)
                BuildScope();
        }

        public void BuildScope()
        {
            if (_isBuilded) return;

            _isBuilded = true;
            ServiceLocator.BuildScope(this);

            BuildComplete();
        }

        protected virtual void BuildComplete() { }
        public virtual void Configurate(IBuilder builder) { }
    }
}
