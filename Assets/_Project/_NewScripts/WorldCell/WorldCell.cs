using UnityEngine;

namespace MyCode
{
    public class WorldCell : MonoBehaviour, IWorldCell
    {
        public int X { get; set; }
        public int Z { get; set; }
        public TeamType Team { get; private set; } = TeamType.None;

        public Vector3 MovePos => transform.position;
        public bool IsLocked => false;

        public void SetTeam(TeamType team)
        {
            Team = team;
        }
    }
}