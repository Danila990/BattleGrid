using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleGridGame
{
    public class GridMap : MonoBehaviour, IGridMap
    {
        [SerializeField] private MultiArray<Cell> _array = new MultiArray<Cell>();
        [SerializeField, HideInInspector] private Vector3 _gridOffset;
        [SerializeField, HideInInspector] private float _offsetCell = 1.2f;

        public ArrayLine<Cell>[] GetCells() => _array.GetAll();

        public void SetupMap(ArrayLine<Cell>[] values, Vector3 gridOffset)
        {
            _gridOffset = gridOffset;
            _array.Set(values);
        }

        public Vector2Int GetSize() => _array.SizeGrid;

        public ICell GetCellAndNear(int x, int z, out ICell[] near, int range = 1)
        {
            near = GetNearCell(x, z, range);
            return GetCell(x, z);
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

        public bool CheckRange(ICell cell1, ICell cell2, int range)
        {
            int xMin = cell1.X - range;
            int xMax = cell1.X + range;
            int zMin = cell1.Z - range;
            int zMax = cell1.Z + range;

            if(cell2.X >= xMin && cell2.X <= xMax && cell2.Z >= zMin && cell2.Z <= zMax)
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

        public ICell GetCell(int x, int z) => _array.Get(x, z);

        public bool FitCell(int x, int z) => _array.Fit(x, z);

        public void GetXZ(Vector3 worldPos, out int x, out int z)
        {
            x = Mathf.FloorToInt((worldPos.x + _gridOffset.x) / _offsetCell + _offsetCell / 2);
            z = Mathf.FloorToInt((worldPos.z + _gridOffset.z) / _offsetCell + _offsetCell / 2);
        }

        public T FindFirstCell<T>(CellType cellType) where T : Cell
        {
            return _array.GetAll()
                .SelectMany(line => line.Values)
                .Where(cell => cell.CellType == cellType)
                .Cast<T>()
                .FirstOrDefault();
        }

        public T[] FindAllCells<T>(CellType cellType) where T : Cell
        {
            return _array.GetAll()
                .SelectMany(line => line.Values)
                .Where(cell => cell.CellType == cellType)
                .Cast<T>()
                .ToArray();
        }
    }
}
