using UnityEngine;
using System.Collections;

public class GunItem : InventoryItem {
    public float fireRate;
    public float scale;
    public bool spread;
    public float minDamage;
    public float maxDamage;
    public float energyUsePerShot;
    public float speed;
    public int projectilesPerShot;
     
    public GunItem() {
        type = InventoryItem.Type.Gun;
    }
}