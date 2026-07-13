using UnityEngine;

namespace ServiceLocator
{
    public class Scope : MonoBehaviour
    {
        [SerializeField] private ScopeInstaller[] _installers;

        private bool _isBuilded = false;

        public void SetupScope(IBuilder builder)
        {
            if(_isBuilded) return;

            _isBuilded = true;
            if (_installers == null) return;

            foreach (var installer in _installers)
                installer.Install(builder);
        }
    }

    public abstract class ScopeInstaller : MonoBehaviour
    {
         public abstract void Install(IBuilder builder);
    }
}
