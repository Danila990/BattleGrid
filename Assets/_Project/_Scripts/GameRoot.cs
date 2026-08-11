using System.Collections;
using UnityServiceLocator;

namespace BattleGridGame
{
    public class GameRoot : ContextRoot
    {
        [Inject] private UnitCreator _unitCreator;
        [Inject] private PlayerUnitInteractor _playerUnitInteraction;

        public override void OnAwake()
        {
            _unitCreator.CreateUnitTest();
        }

        public override void OnStart()
        {
            StartCoroutine(MainGameRoot());
        }

        public override void OnEnd()
        {
            StopAllCoroutines();
        }
        public IEnumerator MainGameRoot()
        {
            //player loop
            while (true)
            {
                yield return _playerUnitInteraction.UnitInteraction();
            }
        }
    }
}