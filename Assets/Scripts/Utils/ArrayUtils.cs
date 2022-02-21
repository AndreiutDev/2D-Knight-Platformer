using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayUtils : MonoBehaviour
{
    public T IsElementInArray<T>(T[] array, T element) where T : class
    {
        foreach (T currentElement in array)
        {
            if (currentElement == element)
            {
                return element;
            }
        }
        return null;
    }
}
