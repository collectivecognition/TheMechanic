using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager : Singleton<InventoryManager> {
    public List<InventoryItem> items = new List<InventoryItem>();
    public GunItem currentGun;

    void Start() {
        AddItem(new PeaShooterItem());
        currentGun = (GunItem)items[0];
    }

    public void NextGun() {
        List<GunItem> guns = items.FindAll(i => i.type == InventoryItem.Type.Gun).Cast<GunItem>().ToList();
        int gunIndex = guns.FindIndex(i => Object.ReferenceEquals(i, currentGun));
        Debug.Log(guns.Count + " guns in inventory");
        Debug.Log("Current gun index = " + gunIndex);
        gunIndex++;
        if (gunIndex >= guns.Count) {
            gunIndex = 0;
        }
        Debug.Log("Next gun index = " + gunIndex);
        currentGun = guns[gunIndex];
    }

    public void AddItem(InventoryItem item) {
        items.Add(item);
    }
}