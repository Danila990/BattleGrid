using System;
using UnityEngine;

namespace MyCode
{
    [CreateAssetMenu]
    public class WorldGridSettings : ScriptableObject
    {
        public float CellSize = 1.2f;
        public int SizeX = 5;
        public int SizeZ = 5;
        public MultiArray<SettingCellInfo> Grid = new MultiArray<SettingCellInfo>(5, 5);

        public Vector3 MiddleOffest()
        {
            float gridWidth = SizeZ * CellSize - CellSize;
            float gridHeight = SizeX * CellSize - CellSize;
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
