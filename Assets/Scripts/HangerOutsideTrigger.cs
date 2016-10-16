using UnityEngine;
using System.Collections;

public class HangerOutsideTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider collider) {
        transform.parent.GetComponent<Hanger>().OpenDoor();
    }

    void OnTriggerExit(Collider collider) {
        transform.parent.GetComponent<Hanger>().CloseDoor();
    }
}
