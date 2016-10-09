using UnityEngine;
using System.Collections.Generic;

public static class GameObjectUtils {
    public static GameObject[] FindChildrenByName(this GameObject gameObject, string name) {
        List<GameObject> childrenWithName = new List<GameObject>();
        foreach (Transform child in gameObject.transform) {
            if (child.name == name) {
                childrenWithName.Add(child.gameObject);
            }
        }
        return childrenWithName.ToArray();
    }
}