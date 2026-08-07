using System.Collections;

namespace UnityServiceLocator
{
    public interface IEntryPoint
    {
        public void GameAwake();
        public void GameStart();
        public void GameUpdate();
        public IEnumerator GameCorutine();
        public void GameEnd();
    }
}
