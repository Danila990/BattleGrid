using UnityEngine;

namespace BattleGridGame
{
    public class CustomCell : Cell
    {
        [SerializeField] private CellType _cellType;
        [SerializeField] private bool _isLoked = false;

        public override CellType CellType => _cellType;
        public override bool IsLocked => _isLoked;
    }
}
