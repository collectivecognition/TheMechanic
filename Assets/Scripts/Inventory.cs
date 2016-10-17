//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;

//public class Inventory : MonoBehaviour {
//    private GameObject inventoryItemPrefab;
//    private List<GameObject> items;
//    private int currentItem = -1;

//    void Awake() {
//        inventoryItemPrefab = Resources.Load<GameObject>("Prefabs/InventoryItem");

//        items = new List<GameObject>();
//        for(int ii = 0; ii < 10; ii++) {
//            GameObject item = Instantiate(inventoryItemPrefab);
//            item.transform.SetParent(transform.Find("Items/Content"), false);
//            item.transform.Find("Text").GetComponent<Text>().text = "This is a test: " + ii;
//            items.Add(item);
//        }

//        ChangeItem(8);
//    }

//    void ChangeItem(int index) {
//        if (currentItem != -1) {
//            items[currentItem].GetComponent<Image>().color = Color.black;
//        }

//        currentItem = index;
//        items[currentItem].GetComponent<Image>().color = Color.red;
//        items[currentItem].GetComponent<Button>().Select();

//        Canvas.ForceUpdateCanvases();

//        transform.Find("Items/Content").GetComponent<ScrollRect>().anchoredPosition =
//            (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
//            - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
//    }
//}
