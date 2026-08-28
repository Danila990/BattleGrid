using System.Linq;
using UnityEngine;

namespace MyCode
{
    public class WorldGridCreator : MonoBehaviour
    {
        [SerializeField] private WorldGridSettings _gridSettings;
        [SerializeField] private WorldCellData[] _worldCellDatas;
        [SerializeField] private WorldGrid _worldGrid;

        public void SetupGrid()
        {
            var grid = CreateGrid();
            _worldGrid.SetupGrid(grid, _gridSettings.MiddleOffest(), _gridSettings.CellSize);
        }

        private WorldCell[,] CreateGrid()
        {
            var grid = _gridSettings.Grid;
            var middleOffset = _gridSettings.MiddleOffest();
            var cellSize = _gridSettings.CellSize;

            var gridParrent = new GameObject("Grid").transform;
            gridParrent.SetParent(transform);

            WorldCell[,] worldCells = new WorldCell[_gridSettings.SizeX, _gridSettings.SizeZ];
            for (int x = 0; x < _gridSettings.SizeX; x++)
            {
                var parrentLine = new GameObject("Line " + x).transform;
                parrentLine.SetParent(gridParrent.transform);
                for (int z = 0; z < _gridSettings.SizeZ; z++)
                {
                    var cellInfo = _gridSettings.Grid.Get(x, z);
                    var worldCell = CreateWorldCell(cellInfo.WordCellType, cellInfo.Team);
                    worldCell.transform.SetParent(parrentLine);
                    worldCell.transform.position = new Vector3(x * cellSize, 0, z * cellSize) - middleOffset;
                    worldCell.X = x;
                    worldCell.Z = z;
                    worldCell.name = $"{x}, {z}";
                    worldCells[x, z] = worldCell;
                }
            }

            return worldCells;
        }

        private WorldCell CreateWorldCell(WordCellType CellType, TeamType team)
        {
            var worldCell = Instantiate(GetWorldCell(CellType));
            worldCell.SetTeam(team);
            return worldCell;
        }

        private WorldCell GetWorldCell(WordCellType wordCellType)
        {
            return _worldCellDatas.FirstOrDefault(_ => _.CellType == wordCellType).GetWordlCell();
        }

        private void OnDrawGizmos()
        {
            if (_gridSettings == null) return;

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