using UnityEngine;
using System.Collections;

public class HealthPotionItem : InventoryItem {
    public float amount = 25f;

    public HealthPotionItem() : base() {
        name = "Health Potion";
        type = InventoryItem.Type.HealthPotion;
        description = "Heals " + amount + " points of health";
    }
}