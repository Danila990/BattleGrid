using UnityServiceLocator;

namespace MyCode
{
    public class PoligonRoot : ContextRoot
    {
        [Inject] private WorldGridCreator _worldGridCreator;

        public override void OnStart()
        {
            _worldGridCreator.SetupGrid();
        }
    }
}