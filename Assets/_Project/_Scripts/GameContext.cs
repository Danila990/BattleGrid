using BattleGridGame;
using UnityEngine;

namespace GameCore.UnityServiceLocator
{
    public class GameContext : SceneContext
    {
        [SerializeField] private GameOptions _gameOptions;

        private GridMap _gridMap;
        private GridUnitCreator _unitCreator;

        protected override void Configurate(IServiceBuilder builder)
        {
            //grid
            _gridMap = builder.RegisteNewGameobject<GridMap>();
            _unitCreator = builder.RegisteNewGameobject<GridUnitCreator>();
            builder.RegisterInstantiate(_gameOptions);
            builder.RegisteNewGameobject<GridUnitInteractor>();
        }

        protected override void BuildComplete()
        {
            _gridMap.CreateGrid();
            _unitCreator.CreateUnitTest();
        }
    }
}