using UnityServiceLocator;
using System.Collections;
using UnityEngine;

namespace BattleGridGame
{
    public class GameContext : SceneContext
    {
        [SerializeField] private GameOptions _gameOptions;
        [SerializeField] private GridOptions _gridOptions;

        protected override void Configurate(IServiceRegister register)
        {
            RegisterRoot<GameRoot>();

            register.RegisterInstantiate(_gameOptions);

            //grid
            register.RegisterInstantiate(_gridOptions);
            register.RegisteNewGameobject<GridMap>();
            register.RegisteNewGameobject<GridUnitCreator>();
            register.RegisteNewGameobject<GridUnitInteractor>();
        }
    }
}