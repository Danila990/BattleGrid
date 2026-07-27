using UnityEngine;

namespace BattleGridGame
{
    [CreateAssetMenu]
    public class GameOptions : ScriptableObject
    {
        [field: SerializeField, Header("Unit")] public Unit EnemyUnit { get; private set; }
        [field: SerializeField] public Unit PlayerUnit { get; private set; }
    }
}
