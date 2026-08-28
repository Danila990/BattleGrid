using UnityEngine;

namespace MyCode
{
    public interface IWorldGrid
    {
        public bool CheckRange(IWorldCell cell1, IWorldCell cell2, int range);
        public bool FitCell(int x, int z);
        public IWorldCell GetCell(int x, int z);
        public IWorldCell GetCell(Vector3 worldPos);
        public IWorldCell GetCell(Vector3 worldPos, out int x, out int z);
        public IWorldCell GetCellAndNear(int x, int z, out IWorldCell[] near, int range = 1);
        public IWorldCell[] GetNearCell(int x, int z, int range = 1);
        public void GetXZ(Vector3 worldPos, out int x, out int z);
        public bool TryGetCell(Vector3 worldPos, out IWorldCell cell);
        public bool TryGetMouseClickCell(out IWorldCell cell);
    }
}