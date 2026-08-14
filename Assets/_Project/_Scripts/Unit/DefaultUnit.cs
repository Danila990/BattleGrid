using System.Collections;
using UnityEngine;

namespace BattleGridGame
{
    public class DefaultUnit : UnitBase
    {
        [SerializeField] private int _damage = 5;

        public override IEnumerator DamageToTarget(ICell targetCell)
        {
            targetCell.Unit.TakeDamage(_damage);
            yield return null;
        }
    }
}