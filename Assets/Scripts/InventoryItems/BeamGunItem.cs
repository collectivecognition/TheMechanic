using UnityEngine;
using System.Collections;

public class BeamGunItem : InventoryItem {
    public Vector3 scale;
    public float minDamagePerSecond;
    public float maxDamagePerSecond;
    public float energyUsePerSecond;
    public string sound;

    public BeamGunItem() {
        type = InventoryItem.Type.BeamGun;
        sound = "";
    }

    override public string description {
        get {
            string text = "";
            text += "MIN DMG:  " + minDamagePerSecond + "\n";
            text += "MAX DMG:  " + maxDamagePerSecond + "\n";
            text += " ENERGY:  " + energyUsePerSecond + "\n";
            return text;
        }
    }
}