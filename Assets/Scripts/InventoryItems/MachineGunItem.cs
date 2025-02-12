﻿using UnityEngine;
using System.Collections;

public class MachineGunItem : GunItem {
    public MachineGunItem() : base() {
        color = new Color(0f, 5f, 0f, 1f);
        energyUsePerShot = 1.5f;
        fireRate = 14f;
        name = "Machine Gun";
        minDamage = 2f;
        maxDamage = 3f;
        projectilesPerShot = 2;
        scale = Vector3.one * 0.7f;
        speed = 120f;
        spread = false;
        sound = "MachineGun";
        projectileName = "Capsule";
        projectileSpacing = 6f;
    }
}
