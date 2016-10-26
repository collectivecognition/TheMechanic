using UnityEngine;
using System.Collections;

public class ShotgunItem : GunItem {
    public ShotgunItem() : base() {
        color = new Color(0f, 0f, 5f, 1f);
        energyUsePerShot = 15f;
        fireRate = 2f;
        name = "Shotgun";
        minDamage = 25f;
        maxDamage = 35f;
        projectilesPerShot = 5;
        scale = 1.5f;
        speed = 80f;
        spread = true;
    }
}
