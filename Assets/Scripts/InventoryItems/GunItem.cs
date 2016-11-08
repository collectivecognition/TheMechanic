using UnityEngine;
using System.Collections;

public class GunItem : InventoryItem {
    public float fireRate;
    public Vector3 scale;
    public bool spread;
    public float range;
    public float minDamage;
    public float maxDamage;
    public float energyUsePerShot;
    public float speed;
    public int projectilesPerShot;
    public string projectileName;
    public float projectileSpacing;
    public Vector3[] projectilePath;
    public float waveFrequency;
    public float waveMagnitude;
    public string sound;
     
    public GunItem() {
        type = InventoryItem.Type.Gun;
        projectileName = "Default";
        projectileSpacing = 0f;
        range = 150f;
        sound = "";
        projectilePath = null;
    }

    override public string description {
        get {
            string text = "";
            text += "SPEED:   " + speed + "\n";
            text += "MIN DMG: " + minDamage + "\n";
            text += "MAX DMG: " + maxDamage + "\n";
            text += "ENERGY:  " + energyUsePerShot + "\n";
            text += "RATE:    " + fireRate + "/s\n";
            return text;
        }
    }
}