using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleGridGame.GridEditor
{
    [System.Serializable]
    public class GridCreator : MoodArray<Cell>
    {
        private const string PATH_PREFABS_GRID = "Assets/_Project/Prefabs/GridMap";
        private const string NAME_GRID_LOAD = "t:prefab";

        private const string PATH_PREFABS_CELL = "Assets/_Project/Prefabs/Cells";
        private const string NAME_CELL_LOAD = "Cell t:prefab";
        private const string NAME_CELL = "Cell";

        public GridMap Map;
        public float OffsetCell = 1.1f;

        public GridCreator() 
        {
            LoadPrefabs();
            Map = Object.FindAnyObjectByType<GridMap>();
        }

        public void CreateGrid(CellType[,] cellTypes)
        {
            if (Map != null)
                DestroyGrid();

            CreateNewGrid(cellTypes);
        }

        public void DestroyGrid()
        {
            if (Map == null) return;

            Object.DestroyImmediate(Map.gameObject);
            Map = null;
        }

        public CellType[,] ConvertGridToCellType()
        {
            if (Map == null) return new CellType[0, 0];

            Vector2Int size = Map.GetSize();
            CellType[,] cellTypes = new CellType[size.x, size.y];

            for (int x = 0; x < size.x; x++)
                for (int y = 0; y < size.y; y++)
                    cellTypes[x, y] = Map.GetCell(x, y).CellType;

            return cellTypes;
        }

        private void CreateNewGrid(CellType[,] cellTypes)
        {
            Vector2Int gridSize = new Vector2Int(cellTypes.GetLength(0), cellTypes.GetLength(1));
            Vector3 middleOffset = MiddleOffest(OffsetCell, gridSize);

            string[] guids = AssetDatabase.FindAssets(NAME_GRID_LOAD, new[] { PATH_PREFABS_GRID });
            Map = new GameObject($"GridMap - {guids.Length}").AddComponent<GridMap>();

            ArrayLine<Cell>[] newGrid = new ArrayLine<Cell>[gridSize.x];
            for (int x = 0; x < gridSize.x; x++)
            {
                Transform parrentLine = new GameObject("Line " + x).transform;
                parrentLine.parent = Map.transform;
                newGrid[x].Values = new Cell[gridSize.y];
                for (int y = 0; y < gridSize.y; y++)
                {
                    Cell platform = CreateCell(cellTypes[x, y], x, y, middleOffset);
                    platform.transform.parent = parrentLine;
                    newGrid[x].Values[y] = platform;
                    platform.SetIndex(x, y);
                    platform.gameObject.name = $"{x}, {y}";
                }
            }

            Map.SetupMap(newGrid, middleOffset);
            Undo.RegisterCreatedObjectUndo(Map.gameObject, "Create Grid map");
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        private Cell CreateCell(CellType cellType, int gridX, int gridY, Vector3 spawnOffset)
        {
            string findCellName = $"{NAME_CELL}_{cellType}";
            Cell newCell = Object.Instantiate(Get<Cell>(findCellName));
            newCell.transform.position = new Vector3(gridX * OffsetCell, 0, gridY * OffsetCell) - spawnOffset;
            newCell.transform.parent = Map.transform;
            return newCell;
        }

        private Vector3 MiddleOffest(float cellOffset, Vector2Int gridSize)
        {
            float gridWidth = gridSize.y * cellOffset - cellOffset;
            float gridHeight = gridSize.x * cellOffset - cellOffset;
            return new Vector3(gridWidth, 0, gridHeight) / 2;
        }

        private void LoadPrefabs()
        {
            List<Cell> listCells = new List<Cell>();
            string[] guids = AssetDatabase.FindAssets(NAME_CELL_LOAD, new[] { PATH_PREFABS_CELL });
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Cell prefab = AssetDatabase.LoadAssetAtPath<Cell>(path);

                if (prefab != null)
                    listCells.Add(prefab);
            }

            _array = listCells.ToArray();
        }

    }
}