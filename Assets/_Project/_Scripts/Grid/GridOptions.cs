using UnityEngine;

namespace BattleGridGame
{
    [CreateAssetMenu]
    public class GridOptions : ScriptableObject
    {
        [field: SerializeField] public Vector2Int SizeGrid { get; private set; } = new Vector2Int(5, 5);
        [field: SerializeField] public GridCell CellPrefab { get; private set; }
        [field: SerializeField] public float OffsetCell { get; private set; } = 1.2f;
        [field: SerializeField] public LayerMask GridLayermask { get; private set; }
    }
}
