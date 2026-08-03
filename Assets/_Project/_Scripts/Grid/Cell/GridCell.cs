using UnityEngine;

namespace BattleGridGame
{
    public class GridCell : MonoBehaviour, ICell
    {
        [SerializeField] private MeshRenderer _renderer;

        public int X { get; set; }
        public int Z { get; set; }
        public TeamType Team { get; set; } = TeamType.None;
        public virtual CellType CellType => CellType.Base;
        public virtual Vector3 MovePos => transform.position;
        public virtual bool IsLocked => false;

        public void ResetView()
        {
            _renderer.material.color = Color.white;
        }

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

            ChangeColor(setColor);
        }

        public void UpdateTeamColor()
        {
            ChangeColor(GetStandartColor());
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

        private void ChangeColor(Color color)
        {
            if(_renderer != null)
                _renderer.material.color = color;
        }
    }
}
