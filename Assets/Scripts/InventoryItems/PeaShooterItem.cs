using UnityEngine;
using System.Collections;

public class PeaShooterItem : GunItem {
    public PeaShooterItem() : base() {
        color = new Color(1f, 0f, 1f, 1f);
        energyUsePerShot = 5f;
        fireRate = 0.5f;
        name = "Pea Shooter";
        minDamage = 10f;
        maxDamage = 14f;
        projectilesPerShot = 1;
        scale = 1f;
        speed = 50f;
        spread = false;
    }
}
