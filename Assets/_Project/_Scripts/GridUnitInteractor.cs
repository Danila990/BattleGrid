using System.Collections.Generic;
using UnityEngine;
using UnityServiceLocator;

namespace BattleGridGame
{
    public class GridUnitInteractor : MonoBehaviour
    {
        /*[Inject] private IGridMap _gridMap;

        private CellView _currentCell;

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || _gridMap == null) return;

            if (_gridMap.TryGetMouseClickCell(out CellView clickedCell))
            {
                if (_currentCell == null)
                {
                    if (clickedCell.Unit != null)
                    {
                        _currentCell = clickedCell;
                        ActivateViewCells();
                    }
                    return;
                }
                else
                {
                    DeactivateViewCells();
                    if (clickedCell.Unit != null && clickedCell.Unit.Team != _currentCell.Unit.Team)
                    {
                        BattleUnit(clickedCell);
                        if (_currentCell != null && clickedCell.Unit == null)
                            MoveUnit(clickedCell);
                    }
                    else
                        MoveUnit(clickedCell);

                    _currentCell = null;
                }
            }
        }

        private void MoveUnit(CellView clickedCell)
        {
            foreach (var moveCell in GetMoveCells())
                if(clickedCell.Equals(moveCell))
                {
                    _currentCell.Unit.Movement(clickedCell.transform.position);
                    clickedCell.Unit = _currentCell.Unit;
                    _currentCell.Unit = null;
                    clickedCell.Team = clickedCell.Unit.Team;
                    clickedCell.UpdateTeamColor();
                    break;
                }
        }

        private void BattleUnit(CellView clickedCell)
        {
            foreach (var attackCell in GetAttackCells())
                if (clickedCell.Equals(attackCell))
                {
                    _currentCell.Unit.TakeDamage(clickedCell.Unit.Damage);
                    clickedCell.Unit.TakeDamage(_currentCell.Unit.Damage);

                    if (clickedCell.Unit.IsDead)
                    {
                        Destroy(clickedCell.Unit.gameObject);
                        clickedCell.Unit = null;
                    }

                    if (_currentCell.Unit.IsDead)
                    {
                        Destroy(_currentCell.Unit.gameObject);
                        _currentCell.Unit = null;
                        _currentCell = null;
                    }

                    break;
                }
        }

        private void ActivateViewCells()
        {
            _currentCell.ChangeColor(CellViewType.Select);

            foreach (var moveCells in GetMoveCells())
                moveCells.ChangeColor(CellViewType.Move);

            foreach (var attacCells in GetAttackCells())
                attacCells.ChangeColor(CellViewType.Attack);
        }

        private void DeactivateViewCells()
        {
            _currentCell.ChangeColor(CellViewType.Standart);

            foreach (var moveCells in GetMoveCells())
                moveCells.ChangeColor(CellViewType.Standart);

            foreach (var attacCells in GetAttackCells())
                attacCells.ChangeColor(CellViewType.Standart);
        }

        private CellView[] GetMoveCells()
        {
            _gridMap.GetCellAndNear(_currentCell.X, _currentCell.Z, out CellView[] moveCells, _currentCell.Unit.MoveRange);
            List<CellView> cells = new List<CellView>();
            foreach(CellView cell in moveCells)
                if(cell.Unit == null)
                    cells.Add(cell);

            return cells.ToArray();
        }

        private CellView[] GetAttackCells()
        {
            _gridMap.GetCellAndNear(_currentCell.X, _currentCell.Z, out CellView[] moveCells, _currentCell.Unit.AttackRange);
            List<CellView> cells = new List<CellView>();
            foreach (CellView cell in moveCells)
                if (cell.Unit != null)
                    if(cell.Unit.Team != _currentCell.Unit.Team)
                        cells.Add(cell);

            return cells.ToArray();
        }*/
    }
}
