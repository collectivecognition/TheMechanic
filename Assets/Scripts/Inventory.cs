using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory {
    public List<InventoryItem> items = new List<InventoryItem>();
    public InventoryItem currentGun;
    public bool updated = false;

    public Inventory() {
        AddItemByName("PeaShooterItem"); // Add a default gun
        AddItemByName("LaserGunItem");
        AddItemByName("WaveGunItem");
        currentGun = (InventoryItem)items[0];
    }

    public void NextGun() {
        List<InventoryItem> guns = items.FindAll(i => (i.type == InventoryItem.Type.Gun || i.type == InventoryItem.Type.BeamGun)).Cast<InventoryItem>().ToList();
        int gunIndex = guns.FindIndex(i => UnityEngine.Object.ReferenceEquals(i, currentGun));
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

    public InventoryItem AddItemByName(string itemName) {
        Type type = Type.GetType(itemName);
        InventoryItem inventoryItem = (InventoryItem)Activator.CreateInstance(type);
        AddItem(inventoryItem);
        return inventoryItem;
    }

    public void RemoveItem(int index) {
        items.RemoveAt(index);
    }

    public bool HasItem(string itemName) {
        bool has = false;
        foreach (InventoryItem item in items) {
            string type = item.GetType().ToString();
            if (itemName == type) {
                has = true;
            }
        }
        return has;
    }
}
