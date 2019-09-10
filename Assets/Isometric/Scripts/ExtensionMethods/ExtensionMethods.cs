using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal static class ExtensionMethods
{
    public static T GetAbstract<T>(this GameObject inObj)  where T : class
    {
        return inObj.GetComponents<T>().FirstOrDefault();
    }

    public static T GetInterface<T>(this GameObject inObj) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            Debug.LogError(typeof(T).ToString() + ": is not an actual interface!");
            return null;
        }
        return inObj.GetComponents<T>().FirstOrDefault();
    }

    public static IEnumerable GetInterfaces<T>(this GameObject inObj) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            Debug.LogError(typeof(T).ToString() + ": is not an actual interface!");
            return Enumerable.Empty<T>();
        }
        return inObj.GetComponents<T>();
    }
}