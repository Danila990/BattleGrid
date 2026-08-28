using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleGridGame
{
    public class WorldGrid : MonoBehaviour, IWorldGrid
    {
        public MultiArray<Cell> MultiArray = new MultiArray<Cell>(5, 5);
        public Vector3 GridPositionOffset;
        public float CellSize = 1.2f;
        public Vector2Int GetSize() => MultiArray.Size;

        public ICell GetCellAndNear(int x, int z, out ICell[] near, int range = 1)
        {
            near = GetNearCell(x, z, range);
            return GetCell(x, z);
        }

        public ICell[] GetNear(int x, int z, Vector2Int[] indexs)
        {
            List<ICell> nearCells = new List<ICell>();
            foreach (var index in indexs)
            {
                Vector2Int cellIndex = new Vector2Int(index.x + x, index.y + z);
                if (FitCell(index.x, index.y))
                    nearCells.Add(GetCell(index.x, index.y));
            }

            return nearCells.ToArray();
        }

        public bool CheckRange(ICell cell1, ICell cell2, int range)
        {
            int xMin = cell1.X - range;
            int xMax = cell1.X + range;
            int zMin = cell1.Z - range;
            int zMax = cell1.Z + range;

            if (cell2.X >= xMin && cell2.X <= xMax && cell2.Z >= zMin && cell2.Z <= zMax)
                return true;

            return false;
        }

        public ICell[] GetNearCell(int x, int z, int range = 1)
        {
            List<ICell> nearCells = new List<ICell>();
            int xMin = x - range;
            int xMax = x + range;
            int zMin = z - range;
            int zMax = z + range;

            for (int i = xMin; i <= xMax; i++)
                for (int j = zMin; j <= zMax; j++)
                    if (FitCell(i, j))
                        nearCells.Add(GetCell(i, j));

            return nearCells.ToArray();
        }

        public bool TryGetMouseClickCell(out ICell cell)
        {
            Vector3 mousePosition = Input.mousePosition;
            Camera camera = Camera.main;
            mousePosition.z = camera.nearClipPlane;
            Ray ray = camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
                return TryGetCell(hit.point, out cell);

            cell = null;
            return false;
        }

        public bool TryGetCell(Vector3 worldPos, out ICell cell)
        {
            GetXZ(worldPos, out int x, out int z);
            cell = GetCell(x, z);
            return cell != null;
        }

        public ICell GetCell(Vector3 worldPos)
        {
            GetXZ(worldPos, out int x, out int z);
            return GetCell(x, z);
        }

        public ICell GetCell(Vector3 worldPos, out int x, out int z)
        {
            GetXZ(worldPos, out x, out z);
            return GetCell(x, z);
        }

        public ICell GetCell(int x, int z) => MultiArray.Get(x, z);

        public bool FitCell(int x, int z) => MultiArray.Fit(x, z);

        public void GetXZ(Vector3 worldPos, out int x, out int z)
        {
            x = Mathf.FloorToInt((worldPos.x + GridPositionOffset.x) / CellSize + CellSize / 2);
            z = Mathf.FloorToInt((worldPos.z + GridPositionOffset.z) / CellSize + CellSize / 2);
        }

        public T FindFirstCell<T>(CellType cellType) where T : Cell
        {
            return MultiArray.GetAll()
                .SelectMany(line => line.Values)
                .Where(cell => cell.CellType == cellType)
                .Cast<T>()
                .FirstOrDefault();
        }

        public T[] FindAllCells<T>(CellType cellType) where T : Cell
        {
            return MultiArray.GetAll()
                .SelectMany(line => line.Values)
                .Where(cell => cell.CellType == cellType)
                .Cast<T>()
                .ToArray();
        }
    }
}
