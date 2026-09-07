using BattleGridGame.GridEditor;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace MyCode
{
    public class CustomGridSettingsEditor : EditorWindow
    {
        [SerializeField] private WorldGridSettings _settings;

        private const int WIDTH_BOX = 80;
        private const int HEIGHT_BOX = 60;
        private int _sizeX = 5;
        private int _sizeZ = 5;

        [MenuItem("Tools/World Grid Editor")]
        public static void OpewEditorWindow()
        {
            GetWindow(typeof(CustomGridSettingsEditor));
        }

        public void OnGUI()
        {
            EditorExtension.CustomPropetry(this, "_settings");
            if (_settings == null || _settings.Grid.SizeX < 0 || _settings.Grid.SizeY < 0) return;

            _sizeX = EditorGUILayout.IntField("New Size X", _sizeX);
            _sizeZ = EditorGUILayout.IntField("New Size Z", _sizeZ);
            _sizeX = math.clamp(_sizeX, 0, 12);
            _sizeZ = math.clamp(_sizeZ, 0, 12);
            if (GUILayout.Button("Create New Grid"))
                _settings.Grid = new MultiArray<SettingCellInfo>(_sizeX, _sizeZ);

            EditorGUILayout.Space(10);
            DrawGrid();
            UpdateSizeWindow();
        }

        private void DrawGrid()
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < _settings.Grid.SizeX; x++)
            {
                EditorGUILayout.BeginVertical();
                for (int z = _settings.Grid.SizeY - 1; z >= 0; z--)
                {
                    Rect boxAndTextRect = GUILayoutUtility.GetRect(WIDTH_BOX, HEIGHT_BOX);
                    var cellType = _settings.Grid.GetAll()[x].Values[z].WordCellType;
                    var cellTeam = _settings.Grid.GetAll()[x].Values[z].Team;
                    Color teamColor = Color.white;
                    Color backCellColor = GetCellColor(cellType);
                    backCellColor.a = 0.1f;
                    if (cellTeam == TeamType.Player)
                        teamColor = Color.blue;
                    else if (cellTeam == TeamType.Enemy)
                        teamColor = Color.red;
                    Handles.DrawSolidRectangleWithOutline(boxAndTextRect, backCellColor, teamColor);
                    EditorGUI.LabelField(boxAndTextRect, cellType.ToString(), GetLabelStyle());
                    Rect cellTypeRect = new Rect(boxAndTextRect.position.x + WIDTH_BOX / 5, boxAndTextRect.position.y + 20, WIDTH_BOX, 15);
                    Rect cellTeamRect = new Rect(boxAndTextRect.position.x + WIDTH_BOX / 5, boxAndTextRect.position.y + 40, WIDTH_BOX, 15);
                    _settings.Grid.GetAll()[x].Values[z].WordCellType = (WordCellType)EditorGUI.EnumPopup(cellTypeRect, cellType);
                    _settings.Grid.GetAll()[x].Values[z].Team = (TeamType)EditorGUI.EnumPopup(cellTeamRect, cellTeam);
                    EditorGUILayout.Space(5);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(5);
            }
            EditorGUILayout.EndHorizontal();
        }

        private GUIStyle GetLabelStyle()
        {
            GUIStyle style = new GUIStyle(EditorStyles.label);
            style.alignment = TextAnchor.UpperCenter;
            style.normal.textColor = Color.black;
            style.fontSize = 15;
            return style;
        }

        private void UpdateSizeWindow()
        {
            Vector2Int size = new Vector2Int(_settings.Grid.SizeX, _settings.Grid.SizeY);
            this.minSize = new Vector2(WIDTH_BOX * size.x + 200, HEIGHT_BOX * size.y + 200);
            position = new Rect(position.x, position.y, minSize.x, minSize.y);
        }

        private Color GetCellColor(WordCellType cellType)
        {
            var color = cellType switch
            {
                WordCellType.None => Color.black,
                WordCellType.Random => Color.yellow,
                WordCellType.StartPlayer or WordCellType.StartEnemy => Color.green,
                WordCellType.Default or _ => Color.white,
            };

            return color;
        }
    }
}
