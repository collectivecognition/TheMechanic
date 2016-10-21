using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryUI : MonoBehaviour {
    private int currentItem = 0;
    private int numPerPage = 7;
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

        items[currentItem].GetComponent<Image>().enabled = false;

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
        if (item.type == InventoryItem.Type.Gun) {
            GunItem gun = (GunItem)item;
            string text = "";
            text += "  SPEED:  " + gun.speed + "\n";
            text += "MIN DMG:  " + gun.minDamage + "\n";
            text += "MAX DMG:  " + gun.maxDamage + "\n";
            text += " ENERGY:  " + gun.energyUsePerShot + "\n";
            text += "   RATE:  " + gun.fireRate + "/s\n";
            descriptionText.text = text;
        }

        // Handle pagination scrolling

        int page = Mathf.FloorToInt((float)currentItem / (float)numPerPage);
        int totalPages = Mathf.CeilToInt((float)items.Length / (float)numPerPage) - 1;
        float verticalPos = 1f - ((float)page / (float)totalPages);
        scrollRect.verticalNormalizedPosition = verticalPos;

        // Set highlight on current item

        items[currentItem].GetComponent<Image>().enabled = true;

        // Select item

        if (Input.GetAxisRaw("Action") != 0) {
            if(inventory.items[currentItem].type == InventoryItem.Type.Gun) {
                inventory.currentGun = (GunItem)inventory.items[currentItem];
                inventory.updated = true;
                UIManager.Instance.CloseCurrentUI();
            }
        }
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
        }

        items[currentItem].GetComponent<Image>().enabled = true;
    }
}
