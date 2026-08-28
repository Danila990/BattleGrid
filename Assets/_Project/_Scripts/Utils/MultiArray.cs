using System;
using UnityEngine;

[System.Serializable]
public struct ArrayLine<T>
{
    public T[] Values;
}

[System.Serializable]
public class MultiArray<T>
{
    [SerializeField] protected ArrayLine<T>[] _values;

    public int SizeX => _values.Length;
    public int SizeY => _values[0].Values.Length;

    public Vector2Int Size => new Vector2Int(SizeX, SizeY);

    public MultiArray(int sizeX, int sizeY)
    {
        _values = new ArrayLine<T>[sizeX];
        for (int i = 0; i < sizeX; i++)
            _values[i].Values = new T[sizeY];
    }

    public void Set(ArrayLine<T>[] values) => _values = values;

    public void Change(T newValue, int x, int y)
    {
        _values[x].Values[y] = newValue; 
    }

    public ArrayLine<T>[] GetAll() => _values;

    public T Get(int x, int y)
    {
        if (!Fit(x, y))
            throw new ArgumentException($"Data index error: X-{x}, Y-{y}");

        return _values[x].Values[y];
    }

    public bool Fit(int x, int y)
    {
        if (x < 0 || y < 0 || x >= SizeX || y >= SizeY)
            return false;

        return true;
    }

    public T[,] Convert()
    {
        T[,] newArray = new T[SizeX, SizeY];
        for (int x = 0; x < SizeX; x++)
            for (int y = 0; y < SizeY; y++)
                newArray[x, y] = _values[x].Values[y];

        return newArray;
    }
}