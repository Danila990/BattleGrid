using BattleGridGame;
using UnityEngine;

namespace UnityServiceLocator
{
    public class GameInstaller : ScopeInstaller
    {
        [SerializeField] private GridMap _gridMap;
        [SerializeField] private GridGenerator _gridGenerator;
        [SerializeField] private GridUnitCreator _unitCreator;
        [SerializeField] private Mouse3D _mouse3DPrefab;

        public override void Install(IBuilder builder)
        {
            builder.Register(_gridMap);
            builder.Register(_unitCreator);
            builder.RegisterInstantiate(_mouse3DPrefab);
        }

        private void Awake()
        {
            _gridGenerator.GenerateGrid();
            _unitCreator.CreateUnit(_gridMap);
        }
    }
}