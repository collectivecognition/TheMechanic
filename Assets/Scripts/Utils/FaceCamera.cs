using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {
    Transform cameraTransform;

    void Update() {
        transform.LookAt(GameManager.Instance.cameraObject.transform);
    }
}