using UnityEngine;

namespace MyCode
{
    public interface IWorldCell
    {
        public int X { get; }
        public int Z { get; }
        public Vector3 MovePos { get; }
        public bool IsLocked { get; }
        public TeamType Team { get; }

        public void SetTeam(TeamType team);
    }
}