using UnityEngine;
using System.Collections;

public class LaserGunItem : BeamGunItem {
    public LaserGunItem() : base() {
        name = "Laser Gun";
        color = new Color(5f, 2.5f, 0f, 1f);
        scale = Vector3.one * 1.5f;
        minDamagePerSecond = 1f;
        maxDamagePerSecond = 4f;
        energyUsePerSecond = 10f;
    }
}
