using System;
using UnityEngine;

namespace MyCode
{
    [CreateAssetMenu]
    public class WorldGridSettings : ScriptableObject
    {
        public float CellSize = 1.2f;
        public MultiArray<SettingCellInfo> Grid = new MultiArray<SettingCellInfo>(5, 5);
        public Vector2Int StartPlayer;
        public Vector2Int StartEnemy;

        public Vector3 MiddleOffest()
        {
            float gridWidth = Grid.SizeY * CellSize - CellSize;
            float gridHeight = Grid.SizeX * CellSize - CellSize;
            return new Vector3(gridWidth, 0, gridHeight) / 2;
        }
    }

    [Serializable]
    public struct SettingCellInfo
    {
        public WordCellType WordCellType;
        public TeamType Team;
    }
}
