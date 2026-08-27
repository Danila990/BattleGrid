using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityServiceLocator;

namespace BattleGridGame
{
    public abstract class UnitBase : MonoBehaviour
    {
        [SerializeField] private int _health = 10;
        [SerializeField] private int _rangeAttack = 1;
        [SerializeField] private int _rangeMovement = 1;

        [Inject] protected IWorldGrid _map;

        public ICell UnitCell { get; private set; }
        public TeamType Team { get; private set; }

        public bool IsDead => _health <= 0;

        public void SetupUnit(TeamType team, ICell unitCell)
        {
            Team = team;
            UnitCell = unitCell;
        }

        public abstract IEnumerator DamageToTarget(ICell targetCell);

        public bool InAttackedCell(ICell targetCell)
        {

            return targetCell.Unit != null && targetCell.Unit.Team != Team && _map.CheckRange(UnitCell, targetCell, _rangeAttack);
        }

        public bool InMovementRange(ICell targetCell)
        {
            return targetCell.Unit == null && !targetCell.IsLocked && _map.CheckRange(UnitCell, targetCell, _rangeAttack);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if(_health < 0)
                _health = 0;
        }

        public IEnumerator Movement(ICell moveCell)
        {
            moveCell.SetUnit(this);
            moveCell.SetTeam(Team);
            UnitCell.SetUnit(null);
            UnitCell = moveCell;
            transform.position = moveCell.MovePos;
            yield return null;
        }

        public void Dead()
        {
            if (IsDead)
            {
                UnitCell.SetUnit(null);
                Destroy(gameObject);
            }
        }

        public ICell[] GetMoveCells()
        {
            List<ICell> moveCells = new List<ICell>();
            foreach (ICell cell in _map.GetNearCell(UnitCell.X, UnitCell.Z, _rangeMovement))
                if (cell.Unit == null && !cell.IsLocked)
                    moveCells.Add(cell);

            return moveCells.ToArray();
        }

        public ICell[] GetAttackCells()
        {
            List<ICell> attackCells = new List<ICell>();
            foreach (ICell cell in _map.GetNearCell(UnitCell.X, UnitCell.Z, _rangeAttack))
                if (cell.Unit != null)
                    if (cell.Unit.Team != Team)
                        attackCells.Add(cell);

            return attackCells.ToArray();
        }
    }
}