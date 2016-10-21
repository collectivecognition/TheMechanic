using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour {
    private int currentItem = 0;
    private int numPerPage = 7;
    private Transform[] items;

    private GameObject inventoryItemPrefab;

    void Awake() {
        inventoryItemPrefab = Resources.Load<GameObject>("Prefabs/InventoryItem");
    }

    void Update() {
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
    }

    public void Refresh() {
        Transform container = transform.Find("Canvas/ScrollView/Viewport/Content");

        foreach (Transform child in container) {
            GameObject.Destroy(child.gameObject);
        }

        items = new Transform[InventoryManager.Instance.items.Count];

        for (int ii = 0; ii < InventoryManager.Instance.items.Count; ii++) {
            InventoryItem item = InventoryManager.Instance.items[ii];
            GameObject o = (GameObject)Instantiate(inventoryItemPrefab, container, false);
            o.transform.GetComponentInChildren<Text>().text = item.name + ii;
            items[ii] = o.transform;
        }

        int extras = numPerPage - items.Length % numPerPage;
        for(int ii = 0; ii < extras; ii++) {
            GameObject o = (GameObject)Instantiate(inventoryItemPrefab, container, false);
        }

        items[currentItem].GetComponent<Image>().enabled = true;
    }
}
