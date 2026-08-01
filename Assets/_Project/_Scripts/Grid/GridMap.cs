using UnityServiceLocator;
using System.Collections.Generic;
using UnityEngine;

namespace BattleGridGame
{

    public class GridMap : MonoBehaviour
    {
        [Inject] private GridOptions _options;

        private GridCell[,] _gridCells;
        private Vector3 _gridOffset;

        public int SizeX => _gridCells.GetLength(0);
        public int SizeZ => _gridCells.GetLength(1);

        public void CreateGrid()
        {
            _gridOffset = GridMiddleOffset() + transform.position;
            _gridCells = CreateGrid(_gridOffset);
        }

        public GridCell GetCellAndNear(Vector3 worldPos, out GridCell[] near)
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

        public bool TryGetMouseClickCell(out GridCell cell)
        {
            Vector3 mousePosition = Input.mousePosition;
            Camera camera = Camera.main;
            mousePosition.z = camera.nearClipPlane;
            Ray ray = camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, _options.GridLayermask))
                return TryGetCell(hit.point, out cell);

            cell = null;
            return false;
        }

        public GridCell GetCellAndNear(int x, int z, out GridCell[] near, int range = 1)
        {
            List<GridCell> nearCells = new List<GridCell>();
            int xMin = x - range;
            int xMax = x + range;
            int zMin = z - range;
            int zMax = z + range;

            for (int i = xMin; i <= xMax; i++)
                for (int j = zMin; j <= zMax; j++)
                    if (FitCell(i, j))
                        nearCells.Add(GetCell(i, j));

            GridCell centerCell = GetCell(x, z);
            nearCells.Remove(centerCell);
            near = nearCells.ToArray();
            return centerCell;
        }

        public bool TryGetCell(Vector3 worldPos, out GridCell cell)
        {
            GetXZ(worldPos, out int x, out int z);
            cell = GetCell(x, z);
            return cell != null;
        }

        public GridCell GetCell(Vector3 worldPos)
        {
            GetXZ(worldPos, out int x, out int z);
            return GetCell(x, z);
        }

        public GridCell GetCell(Vector3 worldPos, out int x, out int z)
        {
            GetXZ(worldPos, out x, out z);
            return GetCell(x, z);
        }

        public GridCell GetCell(int x, int z)
        {
            if (!FitCell(x, z))
            {
                Debug.LogError($"Cell not found: X-{x}, Z-{z}");
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
            x = Mathf.FloorToInt((worldPos.x + _gridOffset.x) / _options.OffsetCell + _options.OffsetCell / 2);
            z = Mathf.FloorToInt((worldPos.z + _gridOffset.z) / _options.OffsetCell + _options.OffsetCell / 2);
        }

        private GridCell[,] CreateGrid(Vector3 _gridOffset)
        {
            var gridCells = new GridCell[_options.SizeGrid.x, _options.SizeGrid.y];
            for (int x = 0; x < _options.SizeGrid.x; x++)
            {
                for (int z = 0; z < _options.SizeGrid.y; z++)
                {
                    GridCell instantiateCell = InstantiateCell(x, z);
                    instantiateCell.transform.position = new Vector3(x * _options.OffsetCell, 0, z * _options.OffsetCell) - _gridOffset;
                    gridCells[x, z] = instantiateCell;
                }
            }

            return gridCells;
        }
        private GridCell InstantiateCell(int x, int z)
        {
            GridCell newCell = Instantiate(_options.CellPrefab);
            newCell.name = $"X-{x}, Z-{z}";
            newCell.X = x;
            newCell.Z = z;
            newCell.transform.parent = transform;
            return newCell;
        }

        private Vector3 GridMiddleOffset()
        {
            float sizeX = _options.SizeGrid.x * _options.OffsetCell - _options.OffsetCell;
            float sizeZ = _options.SizeGrid.y * _options.OffsetCell - _options.OffsetCell;
            return new Vector3(sizeX, 0, sizeZ) / 2;
        }
    }
}
