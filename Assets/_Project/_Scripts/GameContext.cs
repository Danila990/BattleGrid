using UnityServiceLocator;
using UnityEngine;

namespace BattleGridGame
{
    public class GameContext : SceneContext
    {
        [SerializeField] private WorldGrid _mapPrefab;
        [SerializeField] private UnitCreator _unitCreator;
        [SerializeField] private PlayerStepCounter _playerStepCounter;

        protected override void Configurate(IServiceRegister register)
        {
            RegisterSceneRoot<GameRoot>();
            register.RegisterInstantiate<WorldGrid, IWorldGrid>(_mapPrefab);
            register.RegisteNewGameobject<PlayerUnitInteractor>();
            register.Register(_unitCreator);
            register.Register(_playerStepCounter);
        }
    }
}