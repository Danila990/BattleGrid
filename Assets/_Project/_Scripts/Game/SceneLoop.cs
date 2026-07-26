using System.Collections;
using UnityEngine;

namespace BattleGridGame
{
    public abstract class SceneLoop : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return GameStart();
            yield return GameLoop();
            yield return GameEnd();
        }

        protected virtual IEnumerator GameStart() { yield return null; }
        protected virtual IEnumerator GameLoop() { yield return null; }
        protected virtual IEnumerator GameEnd() { yield return null; }
    }
}
