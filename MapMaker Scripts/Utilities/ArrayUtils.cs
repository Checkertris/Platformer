using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUtils
{

public static class ArrayUtils 
{
 
     public static Transform[] SortTransforms(this Transform[] array)
    {
        int[] unsorted = new int[array.Length];
        List<int> sorted = new List<int>();
        int[] index = new int[array.Length];
        Transform[] newArray = new Transform[array.Length];

       foreach (Transform v in array)
       {
            Vector3 pos = v.position;
            float fnum = pos.x + pos.z * 10;
            int num = (int)fnum;
            sorted.Add(num);
       }

        sorted.CopyTo(unsorted);
        sorted.Sort();

        for(int i = 0; i < array.Length; i++)
        {
            index[i] = sorted.IndexOf(unsorted[i]);   

        }

        for(int i = 0; i < array.Length; i++)
        {
            newArray[index[i]] = array[i];

        }
        return newArray;

    }

  public static Vector3[] SortVectors(this Vector3[] array)
    {
        int[] unsorted = new int[array.Length];
        List<int> sorted = new List<int>();
        int[] index = new int[array.Length];
        Vector3[] newArray = new Vector3[array.Length];
       foreach (Vector3 v in array)
       {
            float fnum = v.x + v.z * 10;
            int num = (int)fnum;
            sorted.Add(num);
       }

        sorted.CopyTo(unsorted);
        sorted.Sort();

        for(int i = 0; i < array.Length; i++)
        {
            index[i] = sorted.IndexOf(unsorted[i]);   

        }

        for(int i = 0; i < array.Length; i++)
        {
            newArray[index[i]] = array[i];

        }
        return newArray;

    }


public static T[] IndexRemove<T>(this T[] original, int index)
{
    T[] newArray = new T[original.Length-1];

    for (int i = 0; i < index; i++)
    {
        newArray[i] = original[i];
    }

    for (int i = index+1; i < original.Length; i++)
    {
        newArray[i-1] = original[i];
    }

    return newArray;
}


    public static T[] Add<T>(this T[] original)
{
    T[] newArray = new T[original.Length+1];
    for (int i = 0; i < original.Length; i++)
    {
        newArray[i] = original[i];
    }

    T instance = System.Activator.CreateInstance<T>();
    newArray[original.Length] = instance;

    return newArray;

}

    public static T[] Add<T>(this T[] original, T newItem)
{
    T[] newArray = new T[original.Length+1];
    for (int i = 0; i < original.Length; i++)
    {
        newArray[i] = original[i];
    }

    newArray[original.Length] = newItem;

    return newArray;

}

    

public static T[] Resize<T>(this T[] original, int clamp)
{

        if (original.Length == clamp)
        {
            return original;
        }
        else
        {
            T[] newArray = new T[clamp];
            int count;

            //assign value for count
            if (original.Length < clamp)
            {
                count = original.Length;

            }
            else
            {
                count = clamp;
            }

            //assign values for placeholder
            for (int i = 0; i < count; i++)
            {
                newArray[i] = original[i];
            }

            return newArray;
        }

    }









}


}