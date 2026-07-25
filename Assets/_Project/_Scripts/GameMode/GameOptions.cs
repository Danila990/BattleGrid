using UnityEngine;

namespace BattleGridGame
{
    [CreateAssetMenu]
    public class GameOptions : ScriptableObject
    {
        [Header("Unit")]
        [field: SerializeField] public Unit EnemyUnit { get; private set; }
        [field: SerializeField] public Unit PlayerUnit { get; private set; }

        [Header("Grid")]
        [field: SerializeField] public Vector2Int SizeGrid { get; private set; } = new Vector2Int(3, 3);
        [field: SerializeField] public Cell CellPrefab { get; private set; }
        [field: SerializeField] public float OffsetCell { get; private set; } = 1.2f;

    }
}
