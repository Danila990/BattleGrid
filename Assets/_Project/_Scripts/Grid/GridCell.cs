using FractionsGame;
using UnityEngine;

namespace BattleGridGame
{
    public class GridCell : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer;

        public int X { get; set; }
        public int Z { get; set; }
        public Unit Unit { get; set; }
        public TeamType Team { get; set; } = TeamType.None;

        public void ChangeColor(CellViewType viewType)
        {
            Color setColor = viewType switch
            {
                CellViewType.Standart => GetStandartColor(),
                CellViewType.Select => Color.green,
                CellViewType.Attack => Color.red,
                CellViewType.Move => Color.blue,
                _ => Color.white
            };

            _renderer.material.color = setColor;
        }

        public void UpdateTeamColor()
        {
            _renderer.material.color = Team switch
            {
                TeamType.None => Color.white,
                TeamType.Player => Color.skyBlue,
                TeamType.AI_1 => Color.softRed,
                _ => Color.white
            };
        }

        private Color GetStandartColor()
        {
            return Team switch
            {
                TeamType.None => Color.white,
                TeamType.Player => Color.skyBlue,
                TeamType.AI_1 => Color.softRed,
                _ => Color.white
            };
        }
    }

}
