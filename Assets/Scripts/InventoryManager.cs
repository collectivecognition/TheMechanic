using UnityEngine;
using System.Collections;

public class InventoryManager : Singleton<InventoryManager> {
    public Inventory inventory = new Inventory();
}