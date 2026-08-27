using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyCode
{

    public class WorldGrid : MonoBehaviour//, IWorldGrid
    {
        [SerializeField] private WorldGridSettings _gridSettings;

        private WorldCell[,] _worldCells;
        private Vector3 _gridPositionOffset;
        private float _cellSize = 1.2f;

        public int SizeZ => _worldCells.GetLength(1);
        public int SizeX => _worldCells.GetLength(0);

        public void CreateGrid()
        {
            _gridPositionOffset = _gridSettings.MiddleOffest();
            _cellSize = _gridSettings.CellSize;

        }

        public IWorldCell GetCellAndNear(int x, int z, out IWorldCell[] near, int range = 1)
        {
            near = GetNearCell(x, z, range);
            return GetCell(x, z);
        }

        public IWorldCell[] GetNear(int x, int z, Vector2Int[] indexs)
        {
            List<IWorldCell> nearCells = new List<IWorldCell>();
            foreach (var index in indexs)
            {
                Vector2Int cellIndex = new Vector2Int(index.x + x, index.y + z);
                if (FitCell(index.x, index.y))
                    nearCells.Add(GetCell(index.x, index.y));
            }

            return nearCells.ToArray();
        }

        public bool CheckRange(IWorldCell cell1, IWorldCell cell2, int range)
        {
            int xMin = cell1.X - range;
            int xMax = cell1.X + range;
            int zMin = cell1.Z - range;
            int zMax = cell1.Z + range;

            if (cell2.X >= xMin && cell2.X <= xMax && cell2.Z >= zMin && cell2.Z <= zMax)
                return true;

            return false;
        }

        public IWorldCell[] GetNearCell(int x, int z, int range = 1)
        {
            List<IWorldCell> nearCells = new List<IWorldCell>();
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

        public bool TryGetMouseClickCell(out IWorldCell cell)
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

        public bool TryGetCell(Vector3 worldPos, out IWorldCell cell)
        {
            GetXZ(worldPos, out int x, out int z);
            cell = GetCell(x, z);
            return cell != null;
        }

        public IWorldCell GetCell(Vector3 worldPos)
        {
            GetXZ(worldPos, out int x, out int z);
            return GetCell(x, z);
        }

        public IWorldCell GetCell(Vector3 worldPos, out int x, out int z)
        {
            GetXZ(worldPos, out x, out z);
            return GetCell(x, z);
        }

        public IWorldCell GetCell(int x, int z)
        {
            if (!FitCell(x, z))
                throw new ArgumentException($"Data index error: X-{x}, Z-{z}");

            return _worldCells[x, z];
        }

        public bool FitCell(int x, int z)
        {
            if (x < 0 || z < 0 || x >= SizeX || z >= SizeZ)
                return false;

            return true;
        }

        public void GetXZ(Vector3 worldPos, out int x, out int z)
        {
            x = Mathf.FloorToInt((worldPos.x + _gridPositionOffset.x) / _cellSize + _cellSize / 2);
            z = Mathf.FloorToInt((worldPos.z + _gridPositionOffset.z) / _cellSize + _cellSize / 2);
        }

        private void OnDrawGizmos()
        {
            if (_gridSettings == null)  return;

            Vector3 middleOffset = _gridSettings.MiddleOffest();
            float zStart = -_gridSettings.CellSize / 2;
            float zEnd = (_gridSettings.CellSize / 3) + _gridSettings.CellSize * _gridSettings.SizeZ - 1;

            float xStart = -_gridSettings.CellSize / 2;
            float xEnd = (_gridSettings.CellSize / 3) + _gridSettings.CellSize * _gridSettings.SizeX - 1;

            for (int x = 0; x < _gridSettings.SizeX + 1; x++)
                Gizmos.DrawLine(new Vector3(x * _gridSettings.CellSize + xStart, 0, zStart) - middleOffset, new Vector3(x * _gridSettings.CellSize + xStart, 0, zEnd) - middleOffset);

            for (int z = 0; z < _gridSettings.SizeZ + 1; z++)
                Gizmos.DrawLine(new Vector3(xStart, 0, z * _gridSettings.CellSize + zStart) - middleOffset, new Vector3(xEnd, 0, z * _gridSettings.CellSize + zStart) - middleOffset);
        }
    }
}