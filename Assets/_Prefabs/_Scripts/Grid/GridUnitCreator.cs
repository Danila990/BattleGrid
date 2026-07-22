using UnityEngine;

namespace BattleGridGame
{
    public class GridUnitCreator : MonoBehaviour
    {
        [SerializeField] private Unit _unitPlayer;
        [SerializeField] private Unit _unitAI;

        private GridMap _gridMap;

        public void CreateUnit(GridMap gridMap)
        {
            _gridMap = gridMap;

            CreateUnit(_gridMap.GetCell(1, 1), _unitPlayer);
            CreateUnit(_gridMap.GetCell(0, 1), _unitPlayer);

            CreateUnit(_gridMap.GetCell(2, 3), _unitAI);
            CreateUnit(_gridMap.GetCell(3, 3), _unitAI);
        }

        private Unit CreateUnit(Cell cell, Unit prefab)
        {
            cell.Team = prefab.Team;
            cell.UpdateTeamColor();
            cell.Unit = Instantiate(prefab, cell.transform.position, Quaternion.identity);
            return cell.Unit;
        }
    }
}
