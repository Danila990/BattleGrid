using System.Collections.Generic;
using UnityEngine;
using UnityServiceLocator;

namespace BattleGridGame
{
    public class PlayerUnitInteractor : MonoBehaviour
    {
        [Inject] private IGridMap _gridMap;

        private ICell _currentSelectCell;

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || _gridMap == null) return;

            if (_gridMap.TryGetMouseClickCell(out ICell clickedCell))
            {
                if (_currentSelectCell == null)
                {
                    if (clickedCell.Unit != null && clickedCell.Team == TeamType.Player)
                    {
                        _currentSelectCell = clickedCell;
                        ActivateViewCells();
                    }
                    return;
                }
                else
                {
                    DeactivateViewCells();
                    if (clickedCell.Unit != null && clickedCell.Unit.Team != _currentSelectCell.Unit.Team)
                    {
                        BattleUnit(clickedCell);
                        if (_currentSelectCell != null && clickedCell.Unit == null)
                            MoveUnit(clickedCell);
                    }
                    else
                        MoveUnit(clickedCell);

                    _currentSelectCell = null;
                }
            }
        }

        private void MoveUnit(ICell clickedCell)
        {
            foreach (var moveCell in GetMoveCells())
                if(clickedCell.Equals(moveCell))
                {
                    _currentSelectCell.Unit.Movement(clickedCell.MovePos);
                    clickedCell.SetUnit(_currentSelectCell.Unit);
                    clickedCell.SetTeam(_currentSelectCell.Team);
                    _currentSelectCell.SetUnit(null);
                    break;
                }
        }

        private void BattleUnit(ICell clickedCell)
        {
            foreach (var attackCell in GetAttackCells())
                if (clickedCell.Equals(attackCell))
                {
                    _currentSelectCell.Unit.TakeDamage(clickedCell.Unit.Damage);
                    clickedCell.Unit.TakeDamage(_currentSelectCell.Unit.Damage);

                    if (clickedCell.Unit.IsDead)
                    {
                        Destroy(clickedCell.Unit.gameObject);
                        clickedCell.SetUnit(null);
                    }

                    if (_currentSelectCell.Unit.IsDead)
                    {
                        Destroy(_currentSelectCell.Unit.gameObject);
                        _currentSelectCell.SetUnit(null);
                        _currentSelectCell = null;
                    }

                    break;
                }
        }

        private void ActivateViewCells()
        {
            _currentSelectCell.SetInteractionColor(CellInteractionType.Select);

            foreach (var moveCells in GetMoveCells())
                moveCells.SetInteractionColor(CellInteractionType.Move);

            foreach (var attacCells in GetAttackCells())
                attacCells.SetInteractionColor(CellInteractionType.Attack);
        }

        private void DeactivateViewCells()
        {
            _currentSelectCell.SetInteractionColor(CellInteractionType.Default);

            foreach (var moveCells in GetMoveCells())
                moveCells.SetInteractionColor(CellInteractionType.Default);

            foreach (var attacCells in GetAttackCells())
                attacCells.SetInteractionColor(CellInteractionType.Default);
        }

        private ICell[] GetMoveCells()
        {
            _gridMap.GetCellAndNear(_currentSelectCell.X, _currentSelectCell.Z, out ICell[] moveCells, _currentSelectCell.Unit.MoveRange);
            List<ICell> cells = new List<ICell>();
            foreach(ICell cell in moveCells)
                if(cell.Unit == null && !cell.IsLocked)
                    cells.Add(cell);

            return cells.ToArray();
        }

        private ICell[] GetAttackCells()
        {
            _gridMap.GetCellAndNear(_currentSelectCell.X, _currentSelectCell.Z, out ICell[] moveCells, _currentSelectCell.Unit.AttackRange);
            List<ICell> cells = new List<ICell>();
            foreach (ICell cell in moveCells)
                if (cell.Unit != null)
                    if(cell.Unit.Team != _currentSelectCell.Unit.Team)
                        cells.Add(cell);

            return cells.ToArray();
        }
    }
}
