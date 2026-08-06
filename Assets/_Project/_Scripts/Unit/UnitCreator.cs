 using UnityServiceLocator;
using UnityEngine;

namespace BattleGridGame
{
    public class UnitCreator : MonoBehaviour
    {
        [SerializeField] private Unit _enemyUnit;
        [SerializeField] private Unit _playerUnit;

        [Inject] private IGridMap _gridMap;

        public void CreateUnitTest()
        {
            CreateUnit(_gridMap.GetCell(0, 0), _playerUnit);
            CreateUnit(_gridMap.GetCell(2, 2), _enemyUnit);
        }

        private Unit CreateUnit(ICell cell, Unit prefab)
        {
            Unit unit = Instantiate(prefab, cell.MovePos, Quaternion.identity);
            cell.SetUnit(unit);
            cell.SetTeam(unit.Team);
            return cell.Unit;
        }
    }
}
