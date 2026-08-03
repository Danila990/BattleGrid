using UnityEngine;

namespace BattleGridGame
{
    public class Cell : MonoBehaviour, ICell
    {
        [SerializeField] private MeshRenderer _renderer;

        public int X { get; set; }
        public int Z { get; set; }
        public TeamType Team { get; set; } = TeamType.None;
        public virtual CellType CellType => CellType.Base;
        public virtual Vector3 MovePos => transform.position;
        public virtual bool IsLocked => false;

        public void ResetColor()
        {
            ChangeColor(Color.white);
        }

        public void ChangeColor(Color color)
        {
            if (_renderer != null)
                _renderer.material.color = color;
        }
    }
}
