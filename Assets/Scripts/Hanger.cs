using UnityEngine;
using System.Collections;

public class Hanger : MonoBehaviour {
    private GameObject door;

    void Awake() {
        door = transform.Find("Hangar/Door").gameObject;
    }

    public void OpenDoor() {
        iTween.RotateTo(door, iTween.Hash("x", -90f, "time", 1f));
        iTween.ScaleTo(door, iTween.Hash("y", 20f, "time", 1f));
    }

    public void CloseDoor() {
        iTween.RotateTo(door, iTween.Hash("x", 0f, "time", 1));
        iTween.ScaleTo(door, iTween.Hash("y", 100f, "time", 1f));
    }

}
