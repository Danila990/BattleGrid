using UnityEngine;

namespace BattleGridGame
{

    public class Cell : MonoBehaviour, ICell
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private CellColorOptions _colorOptions;

        [SerializeField, HideInInspector] private int _x, _z;

        public int X => _x;
        public int Z => _z;
        public TeamType Team { get; private set; } = TeamType.None;
        public Unit Unit { get; private set; } = null;
        public virtual CellType CellType => CellType.Base;
        public virtual Vector3 MovePos => transform.position;
        public virtual bool IsLocked => false;

        public void SetIndex(int x, int z)
        {
            _x = x;
            _z = z;
        }

        public void ResetColor()
        {
            ChangeColor(_colorOptions.Default);
        }

        public void SetInteractionColor(CellInteractionType cellInteraction)
        {
            if (cellInteraction == CellInteractionType.Default)
                SetCurrentTeamColor();
            else
                ChangeColor(_colorOptions.GetInteractionColor(cellInteraction));
        }

        public void SetUnit(Unit unit)
        {
            Unit = unit;
        }

        public void SetTeam(TeamType team)
        {
            Team = team;
            SetCurrentTeamColor();
        }

        public void SetCurrentTeamColor()
        {
            ChangeColor(_colorOptions.GetTeamCcolor(Team));
        }

        private void ChangeColor(Color color)
        {
            if (_renderer != null)
                _renderer.material.color = color;
        }
    }
}
