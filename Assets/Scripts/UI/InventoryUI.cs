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
        descriptionText = transform.Find("Inventory/Canvas/Description").GetComponent<Text>();
        scrollRect = transform.Find("Inventory/Canvas/ScrollView").GetComponent<ScrollRect>();
        itemContainer = transform.Find("Inventory/Canvas/ScrollView/Viewport/Content");

        GameManager.Instance.inventoryUI = this;
    }

    void Update() {

        // Close UI

        if(UIManager.Instance.IsOpen("Inventory") && UIButtons.back) {
            UIManager.Instance.CloseUI("Inventory");
        }

        // Open UI

        if(Input.GetKeyDown(KeyCode.I)) {
            UIManager.Instance.OpenUI("Inventory");
        }

        // Return if not open

        if(!UIManager.Instance.IsOpen("Inventory")) { return; }

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

        InventoryItem item = inventory.items[currentItem];

        // Update description text

        descriptionText.text = item.description;

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
