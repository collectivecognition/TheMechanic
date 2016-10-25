using UnityEngine;
using System.Collections;

public class InventoryItem {
    public enum Type {
        Gun,
        Misc
    }

    public Color color;
    public string name;
    public Type type;

    public InventoryItem () {}
}
