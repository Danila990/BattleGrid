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
        public void ResetColor();
        public void ChangeColor(Color color);
    }
}
