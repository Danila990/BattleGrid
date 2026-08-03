using UnityServiceLocator;

namespace BattleGridGame
{
    public class GameRoot : Root
    {
        private GridUnitCreator _unitCreator;

        [Inject]
        public void Construct(GridUnitCreator unitCretor)
        {
            _unitCreator = unitCretor;
        }

        public override void GameInit()
        {
            //_unitCreator.CreateUnitTest();
        }
    }
}