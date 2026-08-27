 using UnityServiceLocator;
using UnityEngine;

namespace BattleGridGame
{
    public class UnitCreator : MonoBehaviour
    {
        [SerializeField] private DefaultUnit _enemyUnit;
        [SerializeField] private DefaultUnit _playerUnit;

        [Inject] private IWorldGrid _gridMap;

        public void CreateUnitTest()
        {
            CreateUnit(_gridMap.GetCell(0, 0), _playerUnit, TeamType.Player);
            CreateUnit(_gridMap.GetCell(2, 2), _enemyUnit, TeamType.AI_1);
        }

        private UnitBase CreateUnit(ICell cell, UnitBase prefab, TeamType teamType)
        {
            UnitBase unit = Instantiate(prefab, cell.MovePos, Quaternion.identity);
            ServiceLocator.Inject(unit);
            unit.SetupUnit(teamType, cell);
            cell.SetUnit(unit);
            cell.SetTeam(unit.Team);
            return cell.Unit;
        }
    }
}
