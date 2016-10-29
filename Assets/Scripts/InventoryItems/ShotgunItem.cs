using UnityEngine;
using System.Collections;

public class ShotgunItem : GunItem {
    public ShotgunItem() : base() {
        color = new Color(0f, 0f, 5f, 1f);
        energyUsePerShot = 15f;
        fireRate = 1.5f;
        range = 35f;
        name = "Shotgun";
        minDamage = 15f;
        maxDamage = 20f;
        projectilesPerShot = 6;
        scale = Vector3.one * 1.25f;
        speed = 80f;
        spread = true;
        sound = "Shotgun";
    }
}
