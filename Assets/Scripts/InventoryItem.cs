using UnityEngine;
using System.Collections;

public class InventoryItem {
    public enum Type {
        Gun,
        BeamGun,
        Misc
    }

    public Color color;
    public string name;
    public string description;
    public Type type;

    public InventoryItem () {}
}
