using UnityServiceLocator;
using UnityEngine;

namespace BattleGridGame
{
    public class GameContext : SceneContext
    {
        [SerializeField] private GridMap _mapPrefab;
        [SerializeField] private UnitCreator _unitCreator;

        protected override void Configurate(IServiceRegister register)
        {
            RegisterRoot<GameEntryPoint>();
            register.RegisterInstantiate<GridMap, IGridMap>(_mapPrefab);
            register.Register(_unitCreator);
            register.RegisteNewGameobject<PlayerUnitInteractor>();
        }
    }
}