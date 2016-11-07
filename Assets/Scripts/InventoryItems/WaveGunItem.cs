using UnityEngine;
using System.Collections;

public class WaveGunItem : GunItem {
    public WaveGunItem() : base() {
        color = new Color(0f, 2.5f, 5f, 1f);
        energyUsePerShot = 1f;
        fireRate = 10f;
        name = "Wave Gun";
        minDamage = 10f;
        maxDamage = 14f;
        projectilesPerShot = 1;
        scale = Vector3.one * 2f;
        speed = 400f;
        spread = false;
        sound = "PeaShooter";
        projectileName = "Default";
        projectileSpacing = 0f;
        waveFrequency = 0.5f;
        waveMagnitude = 20f;
        //projectilePath = new Vector3[] {
        //    new Vector3(0, 0, 0),
        //    new Vector3(5, 0, 5),
        //    new Vector3(10, 0, 10),
        //    new Vector3(5, 0, 15),
        //    new Vector3(0, 0, 20),
        //    new Vector3(-5, 0, 25),
        //    new Vector3(-10, 0, 30),
        //    new Vector3(-5, 0, 35),
        //    new Vector3(0, 0, 40)
        //};
    }
}
