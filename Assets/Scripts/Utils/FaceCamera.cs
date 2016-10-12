using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {
    Transform cameraTransform;

    void Start() {
        cameraTransform = GameObject.Find("Camera").transform;
    }

    void Update() {
        transform.LookAt(cameraTransform);
    }
}