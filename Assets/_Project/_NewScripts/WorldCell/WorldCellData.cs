using UnityEngine;

namespace MyCode
{
    public abstract class WorldCellData : ScriptableObject
    {
        public WordCellType CellType;

        public abstract WorldCell GetWordlCell();
    }
}