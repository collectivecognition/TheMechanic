using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager> {
    private float followDistance;
    private float angle;
    private float closeDistance = 100f;
    private float closeAngle = 30f;
    private float farDistance = 600f;
    private float farAngle = 60f;

    private Transform cameraHarnessTransform;
    private Transform renderCameraTransform;

    public void Start() {
        cameraHarnessTransform = GameManager.Instance.cameraObject.transform.Find("RenderCameraHarness").transform;
        renderCameraTransform = GameManager.Instance.cameraObject.transform.Find("RenderCameraHarness/RenderCamera");
    }
    
    public void ZoomIn(bool instant = false) {
        followDistance = closeDistance;
        angle = closeAngle;
        if (instant && cameraHarnessTransform != null) { 
            cameraHarnessTransform.position = CalculateCameraPosition();
        }
    }

    public void ZoomOut(bool instant = false) {
        followDistance = farDistance;
        angle = farAngle;
        if (instant) {
            cameraHarnessTransform.position = CalculateCameraPosition();
        }
    }

    public void Update () {
        if (GameManager.Instance.player != null) {
            renderCameraTransform.rotation = Quaternion.Lerp(renderCameraTransform.rotation, Quaternion.Euler(angle, 0f, 0f), 3f * Time.deltaTime);
            cameraHarnessTransform.position = Vector3.Lerp(cameraHarnessTransform.position, CalculateCameraPosition(), 3f * Time.deltaTime);
        }
    }

    private Vector3 CalculateCameraPosition() {
        float y = followDistance * Mathf.Cos((90f - angle) * Mathf.Deg2Rad);
        float z = y * Mathf.Tan((90f - angle) * Mathf.Deg2Rad);
        return GameManager.Instance.player.transform.position + new Vector3(0f, y, -z);
    }
}
