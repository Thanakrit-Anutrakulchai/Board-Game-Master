using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// adds extension methods to Unity UI types to make handling them easier
public static class UIExtensions
{
    /*** EXTENSION METHODS ***/
    // remove children objects of the type specified except for the ones in the list provided
    public static void Clear<T>(this ScrollRect scrView, List<T> uncleared)
        where T : Object // equivalent syntax in java is: <T extends Object>
    {
        foreach (T element in scrView.GetComponentsInChildren<T>()) 
        { 
            if (!uncleared.Contains(element)) 
            {
                Object.Destroy(element);
            }
        }
    }

    // above method overloaded to work with one exception without generating a list
    public static void Clear<T>(this ScrollRect scrView, T uncleared)
        where T : Object // equivalent syntax in java is: <T extends Object>
    {
        foreach (T element in scrView.GetComponentsInChildren<T>())
        {
            if (!uncleared.Equals(element))
            {
                Object.Destroy(element);
            }
        }
    }

}
