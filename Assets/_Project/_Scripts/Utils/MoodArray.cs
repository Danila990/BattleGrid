using System;
using UnityEngine;
using Object = UnityEngine.Object;

[System.Serializable]
public class MoodArray<T> where T : Object
{
    [SerializeField] protected T[] _array;

    public int Count => _array.Length;

    public MoodArray(T[] array)
    {
        _array = array;
    }

    public MoodArray()
    {
    }

    public TReturn Get<TReturn>(string key) where TReturn : T
    {
        return (TReturn)Get(key);
    }

    public T Get(string key)
    {
        foreach (var info in _array)
        {
            if (info.name.Equals(key))
                return info;
        }

        throw new NullReferenceException($"The desired object is missing: Key - {key}");
    }
}
