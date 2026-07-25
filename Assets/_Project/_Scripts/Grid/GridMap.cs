using System.Collections.Generic;
using UnityEngine;

namespace BattleGridGame
{
    public class GridMap : MonoBehaviour
    {
        private Cell[,] _gridCells;
        private Vector3 _gridOffset;
        private float _offsetCell;

        public int SizeX => _gridCells.GetLength(0);
        public int SizeZ => _gridCells.GetLength(1);

        public void SetupMap(Cell[,] gridCells, Vector3 gridOffset, float offsetCell)
        {
            _gridCells = gridCells;
            _gridOffset = gridOffset;
            _offsetCell = offsetCell;
        }

        public Cell GetCellAndNear(Vector3 worldPos, out Cell[] near)
        {
            GetXZ(worldPos, out int x, out int z);
            return GetCellAndNear(x, z, out near);
        }

        /*public Cell[] GetNear(int x, int z, Vector2Int[] indexs)
        {
            List<Cell> nearCells = new List<Cell>();
            List<Vector2Int> cellIndex = new List<Vector2Int>();
            foreach (var vector in indexs)
                cellIndex.Add(new Vector2Int(vector.x + x, vector.y + z));

            foreach (var index in cellIndex)
                if (FitCell(index.x, index.y))
                    nearCells.Add(GetCell(index.x, index.y));

            return nearCells.ToArray();
        }*/

        public Cell GetCellAndNear(int x, int z, out Cell[] near, int range = 1)
        {
            List<Cell> nearCells = new List<Cell>();
            int xMin = x - range;
            int xMax = x + range;
            int zMin = z - range;
            int zMax = z + range;

            for (int i = xMin; i <= xMax; i++)
                for (int j = zMin; j <= zMax; j++)
                    if (FitCell(i, j))
                        nearCells.Add(GetCell(i, j));

            Cell centerCell = GetCell(x, z);
            nearCells.Remove(centerCell);
            near = nearCells.ToArray();
            return centerCell;
        }

        public bool TryGetCell(Vector3 worldPos, out Cell cell)
        {
            GetXZ(worldPos, out int x, out int z);
            cell = GetCell(x, z);
            return cell != null;
        }

        public Cell GetCell(Vector3 worldPos)
        {
            GetXZ(worldPos, out int x, out int z);
            return GetCell(x, z);
        }

        public Cell GetCell(Vector3 worldPos, out int x, out int z)
        {
            GetXZ(worldPos, out x, out z);
            return GetCell(x, z);
        }

        public Cell GetCell(int x, int z)
        {
            if (!FitCell(x, z))
            {
                Debug.LogError($"Нет такой Cell: X-{x}, Z-{z}");
                return null;
            }

            return _gridCells[x, z];
        }

        public bool FitCell(int x, int z)
        {
            if (x < 0 || z < 0 || x >= SizeX || z >= SizeZ)
                return false;

            return true;
        }

        public void GetXZ(Vector3 worldPos, out int x, out int z)
        {
            x = Mathf.FloorToInt((worldPos.x + _gridOffset.x) / _offsetCell + _offsetCell / 2);
            z = Mathf.FloorToInt((worldPos.z + _gridOffset.z) / _offsetCell + _offsetCell / 2);
        }
    }
}
