using System.Collections;
using UnityEngine;
using UnityServiceLocator;

namespace BattleGridGame
{

    public class PlayerUnitInteractor : MonoBehaviour
    {
        [Inject] private IGridMap _gridMap;

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
                            ActivateInteractionCells();
                        }
                    }
                    else
                    {
                        DeactivateInteractionCells();
                        if (_playerUnitCell.Unit.InAttackedCell(clickedCell))
                        {
                            yield return _playerUnitCell.Unit.DamageToTarget(clickedCell);
                            if (!clickedCell.Unit.IsDead)
                                yield return clickedCell.Unit.DamageToTarget(_playerUnitCell);
                            else
                                clickedCell.Unit.Dead();

                            if (_playerUnitCell.Unit.IsDead)
                                _playerUnitCell.Unit.Dead();

                            _playerUnitCell = null;
                            break;
                        }
                        else if(_playerUnitCell.Unit.InMovementRange(clickedCell))
                        {
                            yield return _playerUnitCell.Unit.Movement(clickedCell);
                            _playerUnitCell = null;
                            break;
                        }

                        _playerUnitCell = null;
                    }
                }
            }
        }


        private void ActivateInteractionCells()
        {
            _playerUnitCell.SetInteractionColor(CellInteractionType.Select);
            foreach (var moveCell in _playerUnitCell.Unit.GetMoveCells())
                moveCell.SetInteractionColor(CellInteractionType.Move);

            foreach (var attacCell in _playerUnitCell.Unit.GetAttackCells())
                attacCell.SetInteractionColor(CellInteractionType.Attack);
        }

        private void DeactivateInteractionCells()
        {
            _playerUnitCell.SetInteractionColor(CellInteractionType.Default);
            foreach (var moveCell in _playerUnitCell.Unit.GetMoveCells())
                moveCell.SetInteractionColor(CellInteractionType.Default);

            foreach (var attacCell in _playerUnitCell.Unit.GetAttackCells())
                attacCell.SetInteractionColor(CellInteractionType.Default);
        }
    }
}
