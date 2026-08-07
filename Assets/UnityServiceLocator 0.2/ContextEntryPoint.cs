using System.Collections;
using UnityEngine;

namespace UnityServiceLocator
{
    public abstract class ContextEntryPoint : MonoBehaviour, IEntryPoint
    {
        private void Start()
        {
            GameStart();
            StartCoroutine(CorutinerLoop());
        }

        private void Update()
        {
            GameUpdate();
        }

        public virtual IEnumerator GameCorutine()
        {
            yield return null;
        }

        public virtual void GameAwake() { }

        public virtual void GameEnd()
        {
            StopAllCoroutines();
        }

        public virtual void GameUpdate() { }

        public virtual void GameStart() { }

        private IEnumerator CorutinerLoop()
        {
            while (true)
                yield return GameCorutine();
        }
    }
}
