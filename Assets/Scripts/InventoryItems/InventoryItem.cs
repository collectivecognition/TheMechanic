using UnityEngine;
using System.Collections;

public class InventoryItem {
    public enum Type {
        Gun,
        BeamGun,
        Misc,
        HealthPotion,
        EnergyPotion
    }

    public Color color;
    public string name;

    private string _description;
    public virtual string description {
        get {
            return _description;
        }
        set {
            _description = value;
        }
    }
    public Type type;

    public InventoryItem () {}
}
