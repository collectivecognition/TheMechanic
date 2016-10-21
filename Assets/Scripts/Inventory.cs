﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory {
    public List<InventoryItem> items = new List<InventoryItem>();
    public GunItem currentGun;
    public bool updated = false;

    public Inventory() {
        AddItem(new PeaShooterItem()); // Add a default gun
        currentGun = (GunItem)items[0];
    }

    public void NextGun() {
        List<GunItem> guns = items.FindAll(i => i.type == InventoryItem.Type.Gun).Cast<GunItem>().ToList();
        int gunIndex = guns.FindIndex(i => Object.ReferenceEquals(i, currentGun));
        gunIndex++;
        if (gunIndex >= guns.Count) {
            gunIndex = 0;
        }
        currentGun = guns[gunIndex];
    }

    public void AddItem(InventoryItem item) {
        items.Add(item);
        updated = true;
    }
}
