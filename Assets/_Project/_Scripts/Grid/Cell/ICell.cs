using UnityEngine;

namespace BattleGridGame
{
    public interface ICell
    {
        public int X { get; }
        public int Z { get; }
        public CellType CellType { get; }
        public Vector3 MovePos { get; }
        public bool IsLocked { get; }
        public TeamType Team { get; }
        public void ResetView();
        public void ChangeColor(CellViewType viewType);
        public void UpdateTeamColor();
    }
}
