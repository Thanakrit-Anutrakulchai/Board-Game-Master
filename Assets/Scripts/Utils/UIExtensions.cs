using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// adds extension methods to Unity UI types to make handling them easier
public static class UIExtensions
{
    /*** STATIC VARIABLES USED FOR EXTENSIONS ***/
    // keeps track of the chosen item in each scroll view
    private static Dictionary<ScrollRect, UnityEngine.Object> chosenItems;


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


    // attempts to get the chosen item and casts it to the specified type
    //   returns true and assign value to chosen if successful
    //   otherwise returns false
    //   Note: operation considered unsuccessful if item chosen is null
    public static bool GetChosenItem<T>(this ScrollRect scrView, out T chosen)
        where T : Object
    {
        try 
        {
            chosen = chosenItems[scrView] as T; // try to retrive and cast
            return (chosen != null); // guard against null
        } 
        catch 
        {
            chosen = null;
            return false; // return false if failed
        }
    }


    // sets the item currently chosen in a scroll view 
    public static void SetChosenItem(this ScrollRect scrView, Object obj) 
    {
        chosenItems[scrView] = obj;
    }

}
