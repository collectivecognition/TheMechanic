using UnityEngine;
using System.Collections;

public class MiscItem : InventoryItem {
    public MiscItem() : base() {
        description = "";
        type = InventoryItem.Type.Misc;
    }
}
