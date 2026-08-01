
using System.Collections;
using UnityEngine;

namespace UnityServiceLocator
{
    public abstract class Root : MonoBehaviour, IRoot
    {
        private void Start()
        {
            GameStart();
        }

        private void Update()
        {
            GameLoop();
        }

        public void InitRoot()
        {
            GameInit();
            StartCoroutine(GameCorutineLoop());
        }

        public virtual IEnumerator GameCorutineLoop()
        {
            yield return null;
        }

        public virtual void GameInit() { }

        public virtual void GameEnd() { }

        public virtual void GameLoop() { }

        public virtual void GameStart() { }
    }
}
