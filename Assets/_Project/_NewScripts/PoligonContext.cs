using UnityEngine;
using UnityServiceLocator;

namespace MyCode
{
    public class PoligonContext : SceneContext
    {
        [Space(5), Header("Context")]
        [SerializeField] private WorldGrid _worldGrid;
        [SerializeField] private WorldGridCreator _worldGridCreator;

        protected override void Configurate(IServiceRegister register)
        {
            register.Register(_worldGridCreator);
            register.Register<WorldGrid, IWorldGrid>(_worldGrid);

            RegisterSceneRoot<PoligonRoot>();
        }
    }
}