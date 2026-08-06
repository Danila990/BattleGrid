using UnityEngine;

namespace BattleGridGame
{
    public interface ICell
    {
        public int X { get; }
        public int Z { get; }
        public CellType CellType { get; }
        public Vector3 MovePos { get; }
        public TeamType Team { get; }
        public Unit Unit { get; }
        public bool IsLocked { get; }
        public void ResetColor();
        public void SetInteractionColor(CellInteractionType cellInteraction);
        public void SetCurrentTeamColor();
        public void SetTeam(TeamType team);
        public void SetUnit(Unit unit);

    }
}
