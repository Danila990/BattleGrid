using UnityServiceLocator;

namespace BattleGridGame
{
    public class GameRoot : Root
    {
        private GridMap _gridMap;
        private GridUnitCreator _unitCreator;

        [Inject]
        public void Construct(GridMap gridMap, GridUnitCreator unitCretor)
        {
            _gridMap = gridMap;
            _unitCreator = unitCretor;
        }

        public override void GameInit()
        {
            _gridMap.CreateGrid();
            _unitCreator.CreateUnitTest();
        }
    }
}