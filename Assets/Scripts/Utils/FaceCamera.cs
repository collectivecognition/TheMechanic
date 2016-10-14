using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {
    Transform cameraTransform;

    void Awake() {
        cameraTransform = GameObject.Find("Shared/Camera").transform;
    }

    void Update() {
        transform.LookAt(cameraTransform);
    }
}