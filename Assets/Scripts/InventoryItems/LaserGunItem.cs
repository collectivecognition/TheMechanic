using UnityEngine;
using System.Collections;

public class LaserGunItem : BeamGunItem {
    public LaserGunItem() : base() {
        name = "Laser Gun";
        color = new Color(5f, 0f, 0f, 1f);
        scale = Vector3.one;
        minDamagePerSecond = 1f;
        maxDamagePerSecond = 4f;
        energyUsePerSecond = 10f;
    }
}
