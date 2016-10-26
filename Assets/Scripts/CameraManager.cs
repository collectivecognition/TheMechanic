using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager> {
    private float followDistance = 180f;
    private float closeDistance = 50f;
    private float farDistance = 180f;
    
    public void ZoomIn(bool instant = false) {
        followDistance = closeDistance;
        if (instant){ 
            GameManager.Instance.cam.transform.parent.position = CalculateCameraPosition();
        }
    }

    public void ZoomOut(bool instant = false) {
        followDistance = farDistance;
        if (instant) {
            GameManager.Instance.cam.transform.parent.position = CalculateCameraPosition();
        }
    }

    public void Update () {
        if (GameManager.Instance.player != null) {
            GameManager.Instance.cam.transform.parent.position = Vector3.Lerp(GameManager.Instance.cam.transform.parent.position, CalculateCameraPosition(), 3f * Time.deltaTime);
        }
    }

    private Vector3 CalculateCameraPosition() {
        return GameManager.Instance.player.transform.position + Vector3.up * followDistance - Vector3.right * (followDistance + 20f) - Vector3.forward * (followDistance + 20f);
    }
}
