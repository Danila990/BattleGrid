using BattleGridGame;
using UnityEngine;

namespace GameCore.UnityServiceLocator
{
    public class GameEntryPoint : EntryPointScope
    {
        [SerializeField] private GridMap _gridMap;
        [SerializeField] private GridGenerator _gridGenerator;
        [SerializeField] private GridUnitCreator _unitCreator;
        [SerializeField] private Mouse3D _mouse3DPrefab;

        protected override void Configurate(IBuilder builder)
        {
            builder.Register(_gridMap);
            builder.Register(_unitCreator);
            builder.RegisterInstantiate(_mouse3DPrefab);
        }

        protected override void BuildComplete()
        {
            _gridGenerator.GenerateGrid();
            _unitCreator.CreateUnit(_gridMap);
        }
    }
}