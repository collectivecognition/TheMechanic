using UnityEngine;
using System.Collections;

public class EnergyPotionItem : InventoryItem {
    public float amount = 25f;

    public EnergyPotionItem() : base() {
        name = "Energy Potion";
        type = InventoryItem.Type.EnergyPotion;
        description = "Heals " + amount + " points of energy";
    }
}