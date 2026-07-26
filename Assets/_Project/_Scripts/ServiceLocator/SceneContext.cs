
using System.Collections;
using UnityEngine;

namespace GameCore.UnityServiceLocator
{
    [DefaultExecutionOrder(-999)]
    public abstract class SceneContext : ServiceContext
    {
        [SerializeField] private bool _isAutoBuild = true;

        private void Awake()
        {
            if (_isAutoBuild)
                BuildScope();
        }

        public void BuildScope()
        {
            if (_isBuilded) return;

            ServiceLocator.BuildScope(this);
            BuildComplete();
        }

        protected override abstract void Configurate(IServiceBuilder builder);
        protected virtual void BuildComplete() { }
    }
}
