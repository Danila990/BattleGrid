using UnityEngine;

namespace MyCode
{
    public interface IWorldCell
    {
        public int X { get; }
        public int Z { get; }
        public CellType CellType { get; }
        public Vector3 MovePos { get; }
        public bool IsLocked { get; }

    }
}