using UnityEngine;
using System.Collections;

public class DoorPressurePlate : MonoBehaviour {
    private AudioSource audioSource;
    private AudioClip audioClip;
    private GameObject doorLeft;
    private GameObject doorRight;
    private Vector3 doorLeftOriginalPosition;
    private Vector3 doorRightOriginalPosition;
    private float doorOpenOffset = 5f;

    // TODO: Rename, maybe integrate with Door

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Sounds/DoorOpen");
        doorLeft = transform.Find("Model/DoorLeft").gameObject;
        doorRight = transform.Find("Model/DoorRight").gameObject;

        doorLeftOriginalPosition = doorLeft.transform.localPosition;
        doorRightOriginalPosition = doorRight.transform.localPosition;
    }

    public void Open() {
        iTween.MoveTo(doorLeft, iTween.Hash("x", doorLeftOriginalPosition.x - doorOpenOffset, "time", 0.3f, "easeType", iTween.EaseType.linear));
        iTween.MoveTo(doorRight, iTween.Hash("x", doorLeftOriginalPosition.x + doorOpenOffset, "time", 0.3f, "easeType", iTween.EaseType.linear));
        audioSource.PlayOneShot(audioClip);
    }

    public void Close() {
        iTween.MoveTo(doorLeft, iTween.Hash("x", doorLeftOriginalPosition.x, "time", 0.3f, "easeType", iTween.EaseType.linear));
        iTween.MoveTo(doorRight, iTween.Hash("x", doorRightOriginalPosition.x, "time", 0.3f, "easeType", iTween.EaseType.linear));
        audioSource.PlayOneShot(audioClip);
    }
}
