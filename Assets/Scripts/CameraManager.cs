using UnityEngine;
using System;
using System.Collections;

public class CameraManager : Singleton<CameraManager> {
    private float distance;
    private float angle;
    private float closeDistance = 100f;
    private float closeAngle = 30f;
    private float farDistance = 600f;
    private float farAngle = 70f;
    private float zoomTime = 0.5f;

    private bool animating = false;

    private GameObject cameraHarness;
    private GameObject renderCamera;
    private GameObject uiCamera;

    public void Start() {

        // Instantiate camera

        GameObject camera = Instantiate((GameObject)Resources.Load("Prefabs/Camera"));
        camera.transform.SetParent(GameManager.Instance.transform);

        // Register references /w GameManager

        GameManager.Instance.cameraObject = camera;
        GameManager.Instance.renderCamera = camera.transform.Find("RenderCameraHarness/RenderCamera").GetComponent<Camera>();

        // Register internal references

        cameraHarness = GameManager.Instance.cameraObject.transform.Find("RenderCameraHarness").gameObject;
        renderCamera = GameManager.Instance.cameraObject.transform.Find("RenderCameraHarness/RenderCamera").gameObject;
        uiCamera = GameManager.Instance.cameraObject.transform.Find("RenderCameraHarness/UICamera").gameObject;

    }

    public void Update() {
        if (!animating) {
            cameraHarness.transform.position = CalculateCameraPosition(distance, angle);
        }
    }
    
    public void ZoomIn(bool instant = false, Action callback = null) {
        Zoom(closeDistance, closeAngle, instant, callback);
    }

    public void ZoomOut(bool instant = false, Action callback = null) {
        Zoom(farDistance, farAngle, instant, callback);
    }

    private void Zoom(float d, float a, bool instant = false, Action callback = null) {
        distance = d;
        angle = a;

        float time = instant ? 0f : zoomTime;

        Hashtable options = iTween.Hash("position", CalculateCameraPosition(distance, angle), "time", time, "onComplete", "DoneZooming", "onCompleteTarget", gameObject);
        iTween.MoveTo(cameraHarness, options);
        iTween.RotateTo(renderCamera, iTween.Hash("x", angle, "time", time));
        iTween.RotateTo(uiCamera, iTween.Hash("x", angle, "time", time));

        StartCoroutine(Finish(time, callback));
    }

    private Vector3 CalculateCameraPosition(float followDistance, float angle) {
        float y = followDistance * Mathf.Cos((90f - angle) * Mathf.Deg2Rad);
        float z = y * Mathf.Tan((90f - angle) * Mathf.Deg2Rad);
        return GameManager.Instance.player.transform.position + new Vector3(0f, y, -z);
    }

    IEnumerator Finish(float t, Action callback = null) {
        yield return new WaitForSeconds(t);
        animating = false;
        if(callback != null) {
            callback();
        }
    }
}
