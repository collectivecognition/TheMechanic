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
    public string sound;
     
    public GunItem() {
        type = InventoryItem.Type.Gun;
        projectileName = "Default";
        projectileSpacing = 0f;
        range = 150f;
        sound = "";
    }
}