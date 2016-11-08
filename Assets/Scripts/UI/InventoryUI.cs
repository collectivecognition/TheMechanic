using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryUI : MonoBehaviour {
    private int currentItem = 0;
    private int numPerPage = 5;
    private Inventory inventory;

    private Transform[] items;

    private GameObject inventoryItemPrefab;
    private Text descriptionText;
    private ScrollRect scrollRect;
    private Transform itemContainer;

    void Awake() {
        inventoryItemPrefab = Resources.Load<GameObject>("Prefabs/InventoryItem");
        inventory = InventoryManager.Instance.inventory;
        descriptionText = transform.Find("Canvas/Description").GetComponent<Text>();
        scrollRect = transform.Find("Canvas/ScrollView").GetComponent<ScrollRect>();
        itemContainer = transform.Find("Canvas/ScrollView/Viewport/Content");
    }

    void Update() {
        if (inventory.updated) {
            Refresh();
            inventory.updated = false;
        }

        // Disable current selection

        items[currentItem].transform.Find("Highlight").GetComponent<Image>().enabled = false;

        // Navigate up / down with buttons

        if (UIButtons.up) {
            currentItem--;

            if(currentItem < 0) {
                currentItem = items.Length - 1;
            }
        }

        if (UIButtons.down) {
            currentItem++;

            if(currentItem >= items.Length) {
                currentItem = 0;
            }
        }

        // Update description text

        InventoryItem item = inventory.items[currentItem];

        switch(item.type) {
            case InventoryItem.Type.Gun: { 
                    GunItem gun = (GunItem)item;
                    string text = "";
                    text += "  SPEED:  " + gun.speed + "\n";
                    text += "MIN DMG:  " + gun.minDamage + "\n";
                    text += "MAX DMG:  " + gun.maxDamage + "\n";
                    text += " ENERGY:  " + gun.energyUsePerShot + "\n";
                    text += "   RATE:  " + gun.fireRate + "/s\n";
                    descriptionText.text = text;
                    break;
                }

            case InventoryItem.Type.BeamGun: {
                    BeamGunItem gun = (BeamGunItem)item;
                    string text = "";
                    text += "MIN DMG:  " + gun.minDamagePerSecond + "\n";
                    text += "MAX DMG:  " + gun.maxDamagePerSecond + "\n";
                    text += " ENERGY:  " + gun.energyUsePerSecond + "\n";
                    descriptionText.text = text;
                    break;
                }

            default:
                descriptionText.text = item.description;
                break;
        }

        // Handle pagination scrolling

        int page = Mathf.FloorToInt((float)currentItem / (float)numPerPage);
        int totalPages = Mathf.CeilToInt((float)items.Length / (float)numPerPage) - 1;
        float verticalPos = 1f - ((float)page / (float)totalPages);
        scrollRect.verticalNormalizedPosition = verticalPos;

        // Set highlight on current item

        items[currentItem].transform.Find("Highlight").GetComponent<Image>().enabled = true;

        // Select item

        if (UIButtons.action) {
            switch(item.type) {
                case InventoryItem.Type.Gun:
                case InventoryItem.Type.BeamGun:
                    inventory.currentGun = (InventoryItem)inventory.items[currentItem];
                    inventory.updated = true;
                    break;

                case InventoryItem.Type.HealthPotion:
                    PlayerManager.Instance.health.current += ((HealthPotionItem)item).amount;
                    RemoveItem(currentItem);
                    break;

                case InventoryItem.Type.EnergyPotion:
                    PlayerManager.Instance.energy.current += ((EnergyPotionItem)item).amount;
                    RemoveItem(currentItem);
                    break;
            }
        }

        // Close UI

        if(UIButtons.back) {
            UIManager.Instance.CloseUI("Inventory");
        }
    }

    private void RemoveItem(int index) {
        InventoryManager.Instance.inventory.RemoveItem(index);
        currentItem = index > 0 ? index - 1 : inventory.items.Count - 1;
        Refresh();
    }

    public void Refresh() {
        foreach (Transform child in itemContainer) {
            GameObject.Destroy(child.gameObject);
        }

        items = new Transform[inventory.items.Count];

        for (int ii = 0; ii < inventory.items.Count; ii++) {
            InventoryItem item = inventory.items[ii];
            GameObject o = (GameObject)Instantiate(inventoryItemPrefab, itemContainer, false);
            o.transform.GetComponentInChildren<Text>().text = item.name;
            if (Object.ReferenceEquals(inventory.items[ii], inventory.currentGun)) {
                o.transform.GetComponentInChildren<Text>().text += " (E)";
            }
            items[ii] = o.transform;
        }

        int extras = numPerPage - items.Length % numPerPage;
        for(int ii = 0; ii < extras; ii++) {
            GameObject o = (GameObject)Instantiate(inventoryItemPrefab, itemContainer, false);
            o.GetComponent<Image>().enabled = false;
        }

        items[currentItem].transform.Find("Highlight").GetComponent<Image>().enabled = true;
    }
}
