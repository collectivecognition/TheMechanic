using UnityEngine;
using System.Collections;

public class DoorPressurePlate : MonoBehaviour {
    private AudioSource audioSource;
    private AudioClip audioClip;
    private GameObject doorLeft;
    private GameObject doorRight;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Sounds/DoorOpen");
        doorLeft = transform.parent.Find("Model/DoorLeft").gameObject;
        doorRight = transform.parent.Find("Model/DoorRight").gameObject;
    }

    void OnTriggerEnter(Collider collider) {
        iTween.MoveAdd(doorLeft, iTween.Hash("z", 4f, "time", 0.3f, "easeType", iTween.EaseType.easeInOutBack));
        iTween.MoveAdd(doorRight, iTween.Hash("z", -4f, "time", 0.3f, "easeType", iTween.EaseType.easeInOutBack));
        audioSource.PlayOneShot(audioClip);
    }

    void OnTriggerExit(Collider collider) {
        iTween.MoveAdd(doorLeft, iTween.Hash("z", -4f, "time", 0.3f, "easeType", iTween.EaseType.easeInOutBack));
        iTween.MoveAdd(doorRight, iTween.Hash("z", 4f, "time", 0.3f, "easeType", iTween.EaseType.easeInOutBack));
        audioSource.PlayOneShot(audioClip);
    }
}
