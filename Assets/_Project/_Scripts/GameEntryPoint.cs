using UnityServiceLocator;

namespace BattleGridGame
{
    public class GameEntryPoint : ContextEntryPoint
    {
        private UnitCreator _unitCreator;

        [Inject]
        public void Construct(UnitCreator unitCretor)
        {
            _unitCreator = unitCretor;
        }

        public override void GameInit()
        {
            _unitCreator.CreateUnitTest();
        }
    }
}