
using System.Collections;

namespace UnityServiceLocator
{
    public interface IRoot
    {
        public void GameInit();
        public void GameStart();
        public void GameLoop();
        public IEnumerator GameCorutineLoop();
        public void GameEnd();
    }
}
