using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour {
    private GameObject inventoryItemPrefab;

    void Awake() {
        inventoryItemPrefab = Resources.Load<GameObject>("Prefabs/InventoryItem");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            transform.Find("Canvas/ScrollView").GetComponent<ScrollRect>().verticalNormalizedPosition += .1f;
            Debug.Log("Down: " + transform.Find("Canvas/ScrollView").GetComponent<ScrollRect>().verticalNormalizedPosition);
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            transform.Find("Canvas/ScrollView").GetComponent<ScrollRect>().verticalNormalizedPosition -= .1f;
            Debug.Log("Down: " + transform.Find("Canvas/ScrollView").GetComponent<ScrollRect>().verticalNormalizedPosition);
        }
    }

    public void Refresh() {
        Transform container = transform.Find("Canvas/ScrollView/Viewport/Content");

        foreach (Transform child in container) {
            GameObject.Destroy(child.gameObject);
        }

        foreach(InventoryItem item in InventoryManager.Instance.items) {
            GameObject o = (GameObject)Instantiate(inventoryItemPrefab, container, false);
            o.transform.GetComponentInChildren<Text>().text = item.name;
        }
    }
}
