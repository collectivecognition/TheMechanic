using UnityEngine;
using System.Collections;

public class MachineGunItem : GunItem {
    public MachineGunItem() : base() {
        color = new Color(0f, 5f, 0f, 1f);
        energyUsePerShot = 2f;
        fireRate = 14f;
        name = "Machine Gun";
        minDamage = 1f;
        maxDamage = 2f;
        projectilesPerShot = 0;
        scale = new Vector3(0.3f, 0.3f, 0.3f);
        speed = 150f;
        spread = false;
    }
}
