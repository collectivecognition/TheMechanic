using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotificationUI : MonoBehaviour {
    private Transform itemContainerTransform;
    private GameObject itemPrefab;

    private float closeDelay = 2f;
    private IEnumerator coroutine = null;

    void Awake() {
        GameManager.Instance.notificationUI = this;
        itemContainerTransform = transform.Find("Notification/Canvas");
        itemPrefab = Resources.Load<GameObject>("Prefabs/Notification");

        ClearNotifications();
    }

    public void Notify(string message) {

        UIManager.Instance.OpenUI("Notification", false, () => {
            ClearNotifications();
        });

        GameObject o = (GameObject)Instantiate(itemPrefab, itemContainerTransform, false);
        o.GetComponentInChildren<Text>().text = message;

        if (coroutine != null){
            StopCoroutine(coroutine);
        }

        coroutine = CloseAfterDelay();
        StartCoroutine(coroutine);
    }

    private void ClearNotifications() {
        foreach (Transform child in itemContainerTransform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    IEnumerator CloseAfterDelay() {
        yield return new WaitForSeconds(closeDelay);
        UIManager.Instance.CloseUI("Notification");
        coroutine = null;
    }

}
