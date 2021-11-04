using System.Collections.Generic;
using UnityEngine;

static public class ListExtensions
{
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

    static public void ResizeTo<T>(this List<T> list,  int count)
    {
        int currentSize = list.Count;
        int diff = currentSize - count;

        if (diff > 0)
        {
            for(int i = currentSize-1; i >= count; i--)
            {
                list.RemoveAt(i);
            }
        }
        else if (diff < 0)
        {
            for(int i = 0; i < Mathf.Abs(diff); i++)
            {
                list.Add(default);
            }
        }
    }
}
