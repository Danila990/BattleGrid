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

        [Button("Load resources")]
        public void LoadResources()
        {
            
        }
    }
}
