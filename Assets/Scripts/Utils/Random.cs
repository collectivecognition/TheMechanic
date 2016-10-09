using UnityEngine;
using System.Collections.Generic;

public static class ArrayUtils {
    public static T Random<T>(this T[] array) {
        if(array.Length == 0) {
            return default(T);
        }
        int index = UnityEngine.Random.Range(0, array.Length);
        return array[index];
    }
}