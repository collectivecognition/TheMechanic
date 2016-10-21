using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryUI : MonoBehaviour {
    private int currentItem = 0;
    private int numPerPage = 7;
    private Inventory inventory;

    private Transform[] items;

    private GameObject inventoryItemPrefab;

    void Awake() {
        inventoryItemPrefab = Resources.Load<GameObject>("Prefabs/InventoryItem");
        inventory = InventoryManager.Instance.inventory;
    }

    void Update() {
        if (inventory.updated) {
            Refresh();
            inventory.updated = false;
        }

        items[currentItem].GetComponent<Image>().enabled = false;

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

        int page = Mathf.FloorToInt((float)currentItem / (float)numPerPage);
        int totalPages = Mathf.CeilToInt((float)items.Length / (float)numPerPage) - 1;
        float verticalPos = 1f - ((float)page / (float)totalPages);
        transform.Find("Canvas/ScrollView").GetComponent<ScrollRect>().verticalNormalizedPosition = verticalPos;

        items[currentItem].GetComponent<Image>().enabled = true;

        if (Input.GetAxisRaw("Action") != 0) {
            if(inventory.items[currentItem].type == InventoryItem.Type.Gun) {
                inventory.currentGun = (GunItem)inventory.items[currentItem];
                inventory.updated = true;
                UIManager.Instance.CloseCurrentUI();
            }
        }
    }

    public void Refresh() {
        Transform container = transform.Find("Canvas/ScrollView/Viewport/Content");

        foreach (Transform child in container) {
            GameObject.Destroy(child.gameObject);
        }

        items = new Transform[inventory.items.Count];

        for (int ii = 0; ii < inventory.items.Count; ii++) {
            InventoryItem item = inventory.items[ii];
            GameObject o = (GameObject)Instantiate(inventoryItemPrefab, container, false);
            o.transform.GetComponentInChildren<Text>().text = item.name;
            if (Object.ReferenceEquals(inventory.items[ii], inventory.currentGun)) {
                o.transform.GetComponentInChildren<Text>().text += " (E)";
            }
            items[ii] = o.transform;
        }

        int extras = numPerPage - items.Length % numPerPage;
        for(int ii = 0; ii < extras; ii++) {
            GameObject o = (GameObject)Instantiate(inventoryItemPrefab, container, false);
        }

        items[currentItem].GetComponent<Image>().enabled = true;
    }
}
