using UnityEngine;

namespace MyCode
{
    [CreateAssetMenu]
    public class DefaultWorldCellData : WorldCellData
    {
        [SerializeField] private WorldCell _prefab;

        public override WorldCell GetWordlCell()
        {
            return _prefab;
        }
    }
}