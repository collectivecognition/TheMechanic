using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Linq;

public class GameManager : Singleton<GameManager> {
    private Texture2D transitionTexture;
    private int fadeDirection = -1;
    private float fadeSpeed = 1f;
    private int drawDepth = -1000;
    private float alpha = 0f;
    
    public bool gameActive = true;
    public string playerName;

    public Camera cam;
    public Dialogue dialogue;
    public GameObject player;

    void Awake() {

        // Whenever a scene loads

        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode loadSceneMode) => {

            // Initiate fade animation

            fadeDirection = -1;
        };
    }

    void OnGUI() {

        // Fade out screenshot from previous scene
        // TODO: Clean up screenshot when no longer needed

        if (alpha > 0) {
            alpha += fadeSpeed * fadeDirection * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), transitionTexture);
        }
    }

    // Public method for changing between scenes

    public void LoadScene(string sceneName, string spawnPoint = null, Action callback=null) {
        StartCoroutine(PerformSceneLoad(sceneName, spawnPoint, callback));
    }

    private IEnumerator PerformSceneLoad(string sceneName, string spawnPoint, Action callback) {
        
        // This method needs to be a coroutine as we need to wait for the frame to finish rendering
        // before we make a copy of it for our fading animation

        yield return new WaitForEndOfFrame();

        // Make a copy of the current camera and save it to texture

        int w = Screen.width;
        int h = Screen.height;

        RenderTexture rt = new RenderTexture(w, h, 24);
        cam.targetTexture = rt;
        transitionTexture = new Texture2D(w, h, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = rt;
        transitionTexture.ReadPixels(new Rect(0, 0, w, h), 0, 0);
        transitionTexture.Apply();
        cam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // Make the GUI opaque

        alpha = 1f;

        // Register a handler so we can notify the caller when the scene has finished loading

        UnityAction<Scene, LoadSceneMode> handler = null;

        handler = (Scene scene, LoadSceneMode loadSceneMode) => {
            SceneManager.sceneLoaded -= handler; // Unregister handler, only needs call the callback once

            if (spawnPoint != null && spawnPoint.Length > 0) {
                GameObject.Find(spawnPoint).GetComponent<SpawnPoint>().Spawn();
            } else {
                GameObject.FindObjectOfType<SpawnPoint>().Spawn(); // Use the first one by default
            }

            if (callback != null) {
                callback();
            }
        };

        SceneManager.sceneLoaded += handler;

        // Load the requested scene
        SceneManager.LoadScene(sceneName);
    }
}
