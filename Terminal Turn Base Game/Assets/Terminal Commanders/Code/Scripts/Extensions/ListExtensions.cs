using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T FindObjectByName<T>(this List<T> list, string name) where T : Object
    {
        if (list == null || list.Count <= 0) return null;

        foreach (T item in list)
        {
            if (item.name == name)
                return item;
        }

        return null;
    }

    public static bool IsNullOrEmpty<T>(this List<T> list)
    {
        return list.Count <= 0 && list == null;
    }
}
