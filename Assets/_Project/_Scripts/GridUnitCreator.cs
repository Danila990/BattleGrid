using UnityServiceLocator;
using UnityEngine;

namespace BattleGridGame
{
    public class GridUnitCreator : MonoBehaviour
    {
        /*[Inject] private GridMap _gridMap;
        [Inject] private GameOptions _options;

        public void CreateUnitTest()
        {
            CreateUnit(_gridMap.GetCell(1, 1), _options.PlayerUnit);
            CreateUnit(_gridMap.GetCell(0, 1), _options.PlayerUnit);

            CreateUnit(_gridMap.GetCell(2, 3), _options.EnemyUnit);
            CreateUnit(_gridMap.GetCell(3, 3), _options.EnemyUnit);
        }

        private Unit CreateUnit(CellView cell, Unit prefab)
        {
            cell.Team = prefab.Team;
            cell.UpdateTeamColor();
            cell.Unit = Instantiate(prefab, cell.transform.position, Quaternion.identity);
            return cell.Unit;
        }*/
    }
}
