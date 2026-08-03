using UnityServiceLocator;
using UnityEngine;

namespace BattleGridGame
{
    public class GameContext : SceneContext
    {
        [SerializeField] private GameOptions _gameOptions;
        [SerializeField] private GridMap _mapPrefab;

        protected override void Configurate(IServiceRegister register)
        {
            RegisterRoot<GameRoot>();

            register.RegisterInstantiate(_gameOptions);

            //grid
            register.RegisterInstantiate<GridMap, IGridMap>(_mapPrefab);
            register.RegisteNewGameobject<GridUnitCreator>();
            register.RegisteNewGameobject<GridUnitInteractor>();
        }
    }
}