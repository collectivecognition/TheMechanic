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
    public string prefabName;
    public string sound;
     
    public GunItem() {
        type = InventoryItem.Type.Gun;
        prefabName = "Projectile";
        range = 150f;
        sound = "";
    }
}