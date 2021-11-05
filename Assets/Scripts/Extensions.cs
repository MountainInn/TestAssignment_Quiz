using System.Collections.Generic;
using UnityEngine;

static public class IEnumerableExtension
{
    static public void DebugLog<T>(this IEnumerable<T> enumerable)
    {
        string output = string.Empty;

        foreach (T item in enumerable)
        {
            output += item.ToString() + " ";
        }

        Debug.Log(output);
    }
}

static public class ListExtensions
{

    static public List<T> SelectIndexes<T>(this List<T> list, List<int> indexes)
    {
        List<T> selected = new List<T>();

        foreach (var item in indexes)
        {
            selected.Add(list[item]);
        }

        return selected;
    }

    static public T GetRandom<T>(this List<T> list)
    {
        int id = UnityEngine.Random.Range(0, list.Count);
        return list[id];
    }

    static public T ExtractRandom<T>(this List<T> list)
    {
        int id = UnityEngine.Random.Range(0, list.Count);
        T r = list[id];
        list.RemoveAt(id);
        return r;
    }
}


static public class ColorExt
{
    static public Color SetA(this Color col, float a) => new Color(col.r, col.g, col.b, a);
}
