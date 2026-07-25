using UnityEngine;

namespace BattleGridGame
{
    [CreateAssetMenu]
    public class GameOptions : ScriptableObject
    {
        [field: SerializeField, Header("Unit")] public Unit EnemyUnit { get; private set; }
        [field: SerializeField] public Unit PlayerUnit { get; private set; }

        [field: SerializeField, Header("Grid")] public Vector2Int SizeGrid { get; private set; } = new Vector2Int(3, 3);
        [field: SerializeField] public Cell CellPrefab { get; private set; }
        [field: SerializeField] public float OffsetCell { get; private set; } = 1.2f;
        [field: SerializeField] public LayerMask GridLayermask { get; private set; }

    }
}
