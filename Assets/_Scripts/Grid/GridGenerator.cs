using UnityEngine;

namespace BattleGridGame
{

    [RequireComponent(typeof(GridMap), typeof(GridUnitInteractor), typeof(GridUnitCreator))]
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2Int _sizeGrid = new Vector2Int(3, 3);
        [SerializeField] private Cell _cellPrefab;
        [SerializeField] private float _offsetCell = 1.2f;

        public void GenerateGrid()
        {
            Vector3 gridOffset = GridMiddleOffset() + transform.position;
            Cell[,] gridCells = CreateGrid(gridOffset);
            GridMap gridMap = GetComponent<GridMap>();
            gridMap.SetupMap(gridCells, gridOffset, _offsetCell);
        }

        private Cell[,] CreateGrid(Vector3 _gridOffset)
        {
            var gridCells = new Cell[_sizeGrid.x, _sizeGrid.y];
            for (int x = 0; x < _sizeGrid.x; x++)
            {
                for (int z = 0; z < _sizeGrid.y; z++)
                {
                    Cell instantiateCell = InstantiateCell(x, z);
                    instantiateCell.transform.position = new Vector3(x * _offsetCell, 0, z * _offsetCell) - _gridOffset;
                    gridCells[x, z] = instantiateCell;
                }
            }

            return gridCells;
        }
        private Cell InstantiateCell(int x, int z)
        {
            Cell newCell = Instantiate(_cellPrefab);
            newCell.name = $"X-{x}, Z-{z}";
            newCell.X = x;
            newCell.Z = z;
            newCell.transform.parent = transform;
            return newCell;
        }

        private Vector3 GridMiddleOffset()
        {
            float sizeX = _sizeGrid.x * _offsetCell - _offsetCell;
            float sizeZ = _sizeGrid.y * _offsetCell - _offsetCell;
            return new Vector3(sizeX, 0, sizeZ) / 2;
        }
    }
}
