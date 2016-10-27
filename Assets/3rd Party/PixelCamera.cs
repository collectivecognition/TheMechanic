using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/PixelCamera")]
public class PixelCamera : MonoBehaviour {
    public int w = 720;
    private Camera renderCamera;
    int h;
    protected void Start() {
        if (!SystemInfo.supportsImageEffects) {
            enabled = false;
            return;
        }
        renderCamera = GameManager.Instance.cameraObject.transform.Find("RenderCameraHarness/RenderCamera").GetComponent<Camera>();
    }
    void Update() {
        float ratio = ((float)renderCamera.pixelHeight / (float)renderCamera.pixelWidth);
        h = Mathf.RoundToInt(w * ratio);

    }

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        source.filterMode = FilterMode.Bilinear;
        RenderTexture buffer = RenderTexture.GetTemporary(w, h, -1);
        buffer.filterMode = FilterMode.Point;
        Graphics.Blit(source, buffer);
        Graphics.Blit(buffer, destination);
        RenderTexture.ReleaseTemporary(buffer);
    }
}