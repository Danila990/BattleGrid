using BattleGridGame;
using UnityEngine;

namespace UnityScope
{
    public class GameScope : ServiceScope
    {
        [SerializeField] private GridMap _gridMap;
        [SerializeField] private GridGenerator _gridGenerator;
        [SerializeField] private GridUnitCreator _unitCreator;
        [SerializeField] private Mouse3D _mouse3D;

        public override void Configurate(IBuilder builder)
        {
            builder.Register(_gridMap);
            builder.Register(_unitCreator);
            builder.Register(_mouse3D);
        }

        private void Awake()
        {
            _gridGenerator.GenerateGrid();
            _unitCreator.CreateUnit(_gridMap);
        }
    }
}