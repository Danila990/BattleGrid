using UnityEngine;

namespace MyCode
{
    public class WorldCell : MonoBehaviour, IWorldCell
    {
        [field: SerializeField] public int X { get; set; }
        [field: SerializeField] public int Z { get; set; }

        public CellType CellType => CellType.None;
        public Vector3 MovePos => transform.position;
        public bool IsLocked => false;
    }
}