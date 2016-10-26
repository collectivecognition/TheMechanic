using UnityEngine;
using System.Collections;

public class PeaShooterItem : GunItem {
    public PeaShooterItem() : base() {
        color = new Color(0f, 5f, 0f, 1f);
        energyUsePerShot = 5f;
        fireRate = 3f;
        name = "Pea Shooter";
        minDamage = 10f;
        maxDamage = 14f;
        projectilesPerShot = 1;
        scale = 0.5f;
        speed = 80f;
        spread = false;
    }
}
