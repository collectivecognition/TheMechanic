using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : Singleton<GameManager> {
    private Texture2D transitionTexture;
    private float fadeSpeed = 0.5f;
    private int drawDepth = -1000;
    private float alpha = 0f;

    public bool gameActive = true;
    public string playerName;

    // State stuff

    public string currentSceneName;
    public Dictionary<string, int> interactableTriggerCounts = new Dictionary<string, int>();

    // Refs

    public Camera cam;
    public Dialogue dialogue;
    public GameObject player;

    void Awake() {

        // Whenever a scene loads

        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode loadSceneMode) => {
            currentSceneName = scene.name;
        };

        // Hide mouse cursor

        Cursor.visible = false;
    }

    void OnGUI() {

        // Fade out screenshot from previous scene

        if(alpha > 0f && transitionTexture != null) { 
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), transitionTexture);
        }
    }

    // Public method for changing between scenes

    public void LoadScene(string sceneName, string spawnPoint = null, Action callback = null) {
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

        alpha = 1;

        // Pause the game

        gameActive = false;

        // Save reference to old scene

        Scene oldScene = SceneManager.GetActiveScene();
        
        // Load the requested scene, wait for async loading to complete

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        async.allowSceneActivation = false;

        do {
            yield return new WaitForEndOfFrame();
        } while (async.progress < 0.9f); // What the actual fuck, Unity?

        async.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();

        Scene newScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(newScene);
        yield return new WaitForSeconds(0.1f); // FIXME: Scene manager should finish by end of frame, but this doesn't appear to be the case, looks like a race condition in their code
        
        // Clean up old scene

        SceneManager.UnloadScene(oldScene);
        yield return new WaitForEndOfFrame();

        // Spawn player

        if (spawnPoint != null && spawnPoint.Length > 0) {
            GameObject.Find(spawnPoint).GetComponent<SpawnPoint>().Spawn();
        } else {
            GameObject.FindObjectOfType<SpawnPoint>().Spawn(); // Use the first one by default
        }

        // Resume the game

        gameActive = true;

        do {
            alpha -= Time.deltaTime / fadeSpeed;
            yield return 0;
        } while (alpha > 0f);

        // Handle callback

        if (callback != null) {
            callback();
        }
    }
}