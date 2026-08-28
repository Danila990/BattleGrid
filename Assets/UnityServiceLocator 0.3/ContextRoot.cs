using UnityEngine;

namespace UnityServiceLocator
{
    public abstract class ContextRoot : MonoBehaviour, IRootContoller
    {
        private void Start()
        {
            OnStart();
        }

        private void Update()
        {
            OnUpdate();
        }

        public virtual void OnAwake() { }
        public virtual void OnStart() { }
        public virtual void OnEnd() { }
        public virtual void OnUpdate() { }

    }
}
