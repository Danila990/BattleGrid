using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameCore
{
    [CreateAssetMenu]
    public class ResourcesSO : ScriptableObject
    {
        [Serializable]
        public class ObjectInfo
        {
            public string Name;
            public int Id;
            public Object Object;
        }

        [SerializeField] private ObjectInfo[] _resources;

        public GameObject Get(string name) => Get<GameObject>(name);

        public T Get<T>(string name) where T : Object
        {
            return _resources.First(_ => _.Name == name) as T;
        }

#if UNITY_EDITOR

        private const string PATH_TO_LOAD_RESOURCES = "";
        private string[] NAME_FRUIT_LOAD = new string[2] { "t:prefab", "t:asset" };

        [Button("Load resources")]
        public void LoadResources()
        {
            string[] guids = AssetDatabase.FindAssets(NAME_FRUIT_LOAD, new[] { PATH_TO_LOAD_RESOURCES });
        }
#endif
    }
}
