using UnityEngine;
using System.Collections;

public class MachineGunItem : GunItem {
    public MachineGunItem() : base() {
        color = new Color(1f, 0f, 0f, 1f);
        energyUsePerShot = 2f;
        fireRate = 0.075f;
        name = "Machine Gun";
        minDamage = 2f;
        maxDamage = 4f;
        projectilesPerShot = 1;
        scale = 0.5f;
        speed = 150f;
        spread = false;
    }
}
