
using System.Collections;
using UnityEngine;

namespace UnityServiceLocator
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

        private IEnumerator Start()
        {
            yield return GameStart();
            yield return GameLoop();
            yield return GameEnd();
        }

        public void BuildScope()
        {
            if (_isBuilded) return;

            ServiceLocator.BuildScope(this);
            BuildComplete();
        }

        protected override abstract void Configurate(IServiceRegister builder);
        protected virtual void BuildComplete() { }

        protected virtual IEnumerator GameStart() { yield return null; }
        protected virtual IEnumerator GameLoop() { yield return null; }
        protected virtual IEnumerator GameEnd() { yield return null; }
    }
}
