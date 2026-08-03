using System;
using UnityEditor;
using UnityEngine;

namespace BattleGridGame.GridEditor
{
    [Serializable]
    public class ConstructorLine
    {
        public CellType[] lineY;
    }

    public class GridWindowEditor : EditorWindow
    {
        private const string BUTTON_NAME_CREATE_GRID = "Create Grid";
        private const string BUTTON_NAME_DESTROY_GRID = "Destroy Grid";
        private const string BUTTON_NAME_RESET_TYPES_TO_TEST_MAP = "Test map";
        private const string BUTTON_NAME_RESET_TO_BASE_TYPES = "Reset Grid to Base Types";

        [SerializeField] private GridCreator _gridCreator;

        private Vector2Int _sizeGrid = new Vector2Int(1, 1);
        private ConstructorLine[] _linesX;

        [MenuItem("Tools/Grid Window")]
        public static void ShowWindow() => GetWindow<GridWindowEditor>("Grid Window");

        private void OnEnable()
        {
            _gridCreator = new GridCreator();
            
            if (_gridCreator.Map == null)
                ResetGrid();
            else
                ConverGridMapToPreviewGrid();
        }

        private void OnGUI()
        {
            GridEditorExtension.CustomPropetry(this, "_gridCreator");

            EditorGUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            _sizeGrid = EditorGUILayout.Vector2IntField("Grid map SizeGrid", _sizeGrid);
            _sizeGrid = Vector2Int.Max(Vector2Int.Min(_sizeGrid, new Vector2Int(10, 10)), new Vector2Int(0, 0));
            if (GUILayout.Button("Create Prew map", GUILayout.Width(100)))
            {
                if (_sizeGrid.x > 0 && _sizeGrid.y > 0)
                    CreateBaseGridBySize(_sizeGrid);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(BUTTON_NAME_CREATE_GRID))
                _gridCreator.CreateGrid(ConvertPreviewGridToCellTypes());
            if (GUILayout.Button(BUTTON_NAME_DESTROY_GRID))
                _gridCreator.DestroyGrid();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(BUTTON_NAME_RESET_TYPES_TO_TEST_MAP))
                ResetGrid();
            if (GUILayout.Button(BUTTON_NAME_RESET_TO_BASE_TYPES))
                ResetTypes();
            EditorGUILayout.EndHorizontal();

            GridEditorExtension.MidlleText("GridMap", 15, 5);
            DrawPreviewGrid();
            UpdateSizeWindow();
        }

        #region Draw Preview Grid

        private void UpdateSizeWindow()
        {
            Vector2Int size = new Vector2Int(_linesX.Length, _linesX[0].lineY.Length);
            this.minSize = new Vector2(125 * (size.x + 1), 55 * (size.y + 1) + 300);
            if (position.width < minSize.x || position.height < minSize.y)
                position = new Rect(position.x, position.y, minSize.x, minSize.y);
        }

        private CellType[,] ConvertPreviewGridToCellTypes()
        {
            if (_linesX == null || _linesX.Length == 0) return new CellType[0, 0];

            int width = _linesX.Length;
            int height = _linesX[0].lineY.Length;
            CellType[,] platforms = new CellType[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    platforms[x, y] = _linesX[x].lineY[y];

            return platforms;
        }
        private void ConverGridMapToPreviewGrid()
        {
            if (_gridCreator.Map == null) return;

            var cell = _gridCreator.Map.GetCells();

            _linesX = new ConstructorLine[cell.Length];
            for (int x = 0; x < _linesX.Length; x++)
            {
                _linesX[x] = new ConstructorLine();
                _linesX[x].lineY = new CellType[cell[x].Values.Length];
                for (int y = 0; y < _linesX[x].lineY.Length; y++)
                    _linesX[x].lineY[y] = cell[x].Values[y].CellType;
            }
        }

        private void ResetGrid()
        {
            _linesX = new ConstructorLine[3]
            {
                    new ConstructorLine(){
                        lineY = new CellType[3]{ CellType.Base, CellType.Base, CellType.Base }
                    },
                    new ConstructorLine(){
                        lineY = new CellType[3]{ CellType.None, CellType.Base, CellType.None }
                    },
                    new ConstructorLine(){
                        lineY = new CellType[3]{ CellType.Base, CellType.Base, CellType.Base }
                    }
            };
        }

        private void CreateBaseGridBySize(Vector2Int size)
        {
            _linesX = new ConstructorLine[size.x];
            for (int x = 0; x < size.x; x++)
            {
                _linesX[x] = new ConstructorLine();
                _linesX[x].lineY = new CellType[size.y];
                for (int y = 0; y < size.y; y++)
                    _linesX[x].lineY[y] = CellType.Base;
            }
        }

        private void ResetTypes()
        {
            if (_linesX == null) return;

            for (int x = 0; x < _linesX.Length; x++)
                for (int y = 0; y < _linesX[x].lineY.Length; y++)
                    _linesX[x].lineY[y] = CellType.Base;
        }

        private void DrawPreviewGrid()
        {
            if (_linesX == null || _linesX.Length == 0) return;

            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < _linesX.Length; x++)
            {
                EditorGUILayout.BeginVertical();
                for (int y = _linesX[x].lineY.Length - 1; y >= 0 ; y--)
                {
                    DrawCellTextWithLabelField(GUILayoutUtility.GetRect(115, 20), $"Cell: {_linesX[x].lineY[y]}", GetCellColor(_linesX[x].lineY[y]));

                    EditorGUILayout.BeginHorizontal();
                    _linesX[x].lineY[y] = (CellType)EditorGUILayout.EnumPopup(_linesX[x].lineY[y]);

                    if (_gridCreator.Map != null)
                        if (_gridCreator.Map.GetSize() == new Vector2Int(_linesX.Length, _linesX[x].lineY.Length))
                            if (_linesX[x].lineY[y] == _gridCreator.Map.GetCells()[x].Values[y].CellType)
                            {
                                GridCell cell = _gridCreator.Map.GetCells()[x].Values[y];
                                CustomField(cell);
                            }

                    EditorGUILayout.EndHorizontal();

                    if (_gridCreator.Map != null)
                        if (_gridCreator.Map.GetSize() == new Vector2Int(_linesX.Length, _linesX[x].lineY.Length))
                                EditorGUILayout.ObjectField(_gridCreator.Map.GetCells()[x].Values[y], typeof(GridCell), true);

                    EditorGUILayout.Space(5);
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(15);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawCellTextWithLabelField(Rect rect, string text, Color color)
        {
            Handles.color = color;
            Handles.DrawSolidRectangleWithOutline(rect, color, color);

            Rect textRect = new Rect(rect.x, rect.y , rect.width, rect.height);
            Color originalBackgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = color;

            Handles.BeginGUI();
            EditorGUI.LabelField(textRect, text, GetLabelStyle());
            Handles.EndGUI();

            GUI.backgroundColor = originalBackgroundColor;
        }

        private GUIStyle GetLabelStyle()
        {
            GUIStyle style = new GUIStyle(EditorStyles.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.black;
            style.fontSize = 15;
            return style;
        }

        private void CustomField(GridCell cell)
        {
            /*if(cell as FruitCell)
            {
                FruitCell fruitCell = cell as FruitCell;
                FruitType newType = (FruitType)EditorGUILayout.EnumPopup(fruitCell.fruit.fruitType);
                _gridCreator.ChangeFruit(newType, fruitCell);
            }
            else if(cell as TrapCell)
            {
                TrapCell trapCell = cell as TrapCell;
                TrapType newType = (TrapType)EditorGUILayout.EnumPopup(trapCell.trap.trapType);
                _gridCreator.ChangeTrap(newType, trapCell);
            }*/
        }

        private Color GetCellColor(CellType platformType)
        {
            return platformType switch
            {
                CellType.Base => Color.white,
                CellType.None => Color.brown,
                /*CellType.Player => Color.green,
                CellType.Finish => Color.blue,
                CellType.Fruit => Color.yellow,
                CellType.Trap => Color.red,*/
                _ => Color.gray,
            };
        }
        #endregion
    }
}