using UnityEngine;

namespace BattleGridGame
{
    public class Unit : MonoBehaviour
    {
        [field: SerializeField] public TeamType Team { get; private set; }
        [field: SerializeField] public float Health { get; private set; } = 10f;
        [field: SerializeField] public float Damage { get; private set; } = 5f;
        [field: SerializeField] public int MoveRange { get; private set; } = 1;
        [field: SerializeField] public int AttackRange { get; private set; } = 1;

        public bool IsDead  => Health <= 0;

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }

        public void Movement(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}