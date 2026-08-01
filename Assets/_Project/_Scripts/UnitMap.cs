using GameCore.UnityServiceLocator;
using System.Collections.Generic;
using UnityEngine;

namespace BattleGridGame
{
    public class UnitMap : MonoBehaviour
    {
        public class UnitGridInfo
        {
            public int X, Z;
            public Unit Unit;
        }

        [Inject] private GridMap _gridMap;

        private List<UnitGridInfo> _units = new();

        /*public bool CheckUnit(GridCell)
        {
            
        }*/

        private UnitGridInfo FindGridInfo(int x, int z)
        {
            foreach (var info in _units)
                if(info.X == x && info.Z == z)
                    return info;

            return null;
        }

        private UnitGridInfo FindGridInfo(Unit unit)
        {
            foreach (var info in _units)
                if (info.Unit.Equals(unit))
                    return info;

            return null;
        }
    }
}
