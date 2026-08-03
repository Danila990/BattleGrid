using UnityEngine;

namespace BattleGridGame
{
    public interface IGridMap
    {
        public T[] FindAllCells<T>(CellType cellType) where T : Cell;
        public T FindFirstCell<T>(CellType cellType) where T : Cell;
        public bool FitCell(int x, int z);
        public ICell GetCell(int x, int z);
        public ICell GetCell(Vector3 worldPos);
        public ICell GetCell(Vector3 worldPos, out int x, out int z);
        public ICell GetCellAndNear(int x, int z, out ICell[] near, int range = 1);
        public ICell GetCellAndNear(Vector3 worldPos, out ICell[] near);
        public Vector2Int GetSize();
        public void GetXZ(Vector3 worldPos, out int x, out int z);
        public bool TryGetCell(Vector3 worldPos, out ICell cell);
        public bool TryGetMouseClickCell(out ICell cell);
    }
}