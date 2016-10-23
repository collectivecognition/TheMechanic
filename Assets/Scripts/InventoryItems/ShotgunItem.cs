using UnityEngine;
using System.Collections;

public class ShotgunItem : GunItem {
    public ShotgunItem() : base() {
        color = new Color(0f, 0f, 5f, 1f);
        energyUsePerShot = 15f;
        fireRate = 1f;
        name = "Shotgun";
        minDamage = 25f;
        maxDamage = 35f;
        projectilesPerShot = 5;
        scale = 2f;
        speed = 40f;
        spread = true;
    }
}
