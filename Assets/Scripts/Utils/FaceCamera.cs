using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {
    void Update() {
        transform.LookAt(GameManager.Instance.cameraObject.transform.Find("RenderCameraHarness").position); // FIXME: Cache this
    }
}