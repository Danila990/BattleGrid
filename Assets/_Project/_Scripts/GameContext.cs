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
            builder.RegisterResources<Mouse3D>("_Prefabs/Mouse3D");

            //grid
            builder.RegisterInstantiate(_gameOptions);
            _gridMap = builder.RegisteNewGameobject<GridMap>();
            _unitCreator = builder.RegisteNewGameobject<GridUnitCreator>();
            builder.RegisteNewGameobject<GridUnitInteractor>();

        }

        protected override void BuildComplete()
        {
            _gridMap.CreateGrid();
            _unitCreator.CreateUnitTest();
        }
    }
}