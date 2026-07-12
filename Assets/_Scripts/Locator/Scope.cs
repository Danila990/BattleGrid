using UnityEngine;

namespace ServiceLocator
{
    public abstract class Scope : MonoBehaviour
    {
        private bool _isBuilded = false;

        public void SetupScope(IBuilder builder)
        {
            if(_isBuilded) return;

            _isBuilded = true;
            Configurate(builder);
        }

        protected abstract void Configurate(IBuilder builder);
    }
}
