using System.Collections;

namespace UnityServiceLocator
{
    public interface IEntryPoint
    {
        public void GameInit();
        public void GameStart();
        public void GameLoop();
        public IEnumerator GameCorutineLoop();
        public void GameEnd();
    }
}
