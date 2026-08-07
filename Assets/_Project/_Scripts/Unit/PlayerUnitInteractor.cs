using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityServiceLocator;

namespace BattleGridGame
{
    public class PlayerUnitInteractor : MonoBehaviour
    {
        [Inject] private IGridMap _gridMap;
        [Inject] private UnitGridMapEvents _battler;

        private ICell _playerUnitCell;

        public IEnumerator UnitInteraction()
        {
            while (true)
            {
                yield return null;

                if (Input.GetMouseButtonDown(0) && _gridMap.TryGetMouseClickCell(out ICell clickedCell))
                {
                    if (_playerUnitCell == null)
                    {
                        if (clickedCell.Unit != null && clickedCell.Team == TeamType.Player)
                        {
                            _playerUnitCell = clickedCell;
                            ActivateInteractionColorCells();
                        }
                    }
                    else
                    {
                        DeactivateViewCells();
                        if (clickedCell.Unit != null && clickedCell.Unit.Team != TeamType.Player)
                            yield return PlayerUnitAttackUnit(clickedCell);

                        else
                            yield return PlayerUnitMove(clickedCell);

                        _playerUnitCell = null;
                    }
                }
            }
        }

        private IEnumerator PlayerUnitAttackUnit(ICell enemyUnitCell)
        {
            if (_gridMap.CheckRange(_playerUnitCell, enemyUnitCell, _playerUnitCell.Unit.AttackRange))
                yield return _battler.UnitBattle(_playerUnitCell, enemyUnitCell);
        }

        private IEnumerator PlayerUnitMove(ICell clickedCell)
        {
            if(_gridMap.CheckRange(_playerUnitCell, clickedCell, _playerUnitCell.Unit.MoveRange))
                yield return _battler.UnitMove(_playerUnitCell, clickedCell);
        }

        private void ActivateInteractionColorCells()
        {
            _playerUnitCell.SetInteractionColor(CellInteractionType.Select);
            foreach (var moveCell in GetMoveCells())
                moveCell.SetInteractionColor(CellInteractionType.Move);

            foreach (var attacCell in GetAttackCells())
                attacCell.SetInteractionColor(CellInteractionType.Attack);
        }

        private void DeactivateViewCells()
        {
            _playerUnitCell.SetInteractionColor(CellInteractionType.Default);
            foreach (var cell in _gridMap.GetNearCell(_playerUnitCell.X, _playerUnitCell.Z, _playerUnitCell.Unit.GetMaxRange))
                cell.SetInteractionColor(CellInteractionType.Default);
        }

        private ICell[] GetMoveCells()
        {
            List<ICell> cells = new List<ICell>();
            foreach(ICell cell in _gridMap.GetNearCell(_playerUnitCell.X, _playerUnitCell.Z, _playerUnitCell.Unit.MoveRange))
                if(cell.Unit == null && !cell.IsLocked)
                    cells.Add(cell);

            return cells.ToArray();
        }

        private ICell[] GetAttackCells()
        {
            List<ICell> cells = new List<ICell>();
            foreach (ICell cell in _gridMap.GetNearCell(_playerUnitCell.X, _playerUnitCell.Z, _playerUnitCell.Unit.AttackRange))
                if (cell.Unit != null)
                    if(cell.Unit.Team != _playerUnitCell.Unit.Team)
                        cells.Add(cell);

            return cells.ToArray();
        }
    }
}
