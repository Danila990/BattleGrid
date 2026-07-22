using UnityEngine;

namespace GameCore.UnityServiceLocator
{
    [DefaultExecutionOrder(-999)]
    public class Scope : MonoBehaviour
    {
        [SerializeField] private ScopeInstaller[] _installers;

        private bool _isBuilded = false;

        private void Awake()
        {
            ServiceLocator.BuildScope(Configurate);
        }

        private void Configurate(IBuilder builder)
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
