using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager> {
    private float followDistance = 180f;
    private float closeDistance = 50f;
    private float farDistance = 280f;

    private Transform cameraHarnessTransform;

    public void Start() {
        cameraHarnessTransform = GameManager.Instance.cameraObject.transform.Find("RenderCameraHarness").transform;
    }
    
    public void ZoomIn(bool instant = false) {
        followDistance = closeDistance;
        if (instant && cameraHarnessTransform != null) { 
            cameraHarnessTransform.position = CalculateCameraPosition();
        }
    }

    public void ZoomOut(bool instant = false) {
        followDistance = farDistance;
        if (instant) {
            cameraHarnessTransform.position = CalculateCameraPosition();
        }
    }

    public void Update () {
        if (GameManager.Instance.player != null) {
            cameraHarnessTransform.position = Vector3.Lerp(cameraHarnessTransform.position, CalculateCameraPosition(), 3f * Time.deltaTime);
        }
    }

    private Vector3 CalculateCameraPosition() {
        return GameManager.Instance.player.transform.position + Vector3.up * followDistance - Vector3.right * (followDistance + 20f) - Vector3.forward * (followDistance + 20f);
    }
}
