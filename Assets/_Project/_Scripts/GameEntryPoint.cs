using System.Collections;
using UnityServiceLocator;

namespace BattleGridGame
{
    public class GameEntryPoint : ContextEntryPoint
    {
        [Inject] private UnitCreator _unitCreator;
        [Inject] private PlayerUnitInteractor _playerUnitInteraction;

        public override void GameAwake()
        {
            _unitCreator.CreateUnitTest();
        }

        public override IEnumerator GameCorutine()
        {
            //player loop
            while (true)
            {
                yield return _playerUnitInteraction.UnitInteraction();
            }

            yield return base.GameCorutine();
        }
    }
}