using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : Singleton<GameManager> {
    private Texture2D transitionTexture;
    private int fadeDirection = -1;
    private float fadeSpeed = 1f;
    private int drawDepth = -1000;
    private float alpha = 0f;

    public bool gameActive = true;
    public string playerName;

    // State stuff

    public Dictionary<string, int> interactableTriggerCounts = new Dictionary<string, int>();

    // Refs

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

        alpha = 1f;

        // Save reference to old scene

        Scene oldScene = SceneManager.GetActiveScene();

        // Create a new, temporary scene

        Scene tempScene = SceneManager.CreateScene(sceneName);
        SceneManager.SetActiveScene(tempScene);
        
        // Load the requested scene, wait for async loading to complete

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        //async.allowSceneActivation = false;

        do {
            yield return 0;
        } while (async.progress < 0.9f);

        async.allowSceneActivation = true;
        yield return 0; // Wait for one frame

        // Merge the temporary scene and the new scene

        Scene newScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.MergeScenes(tempScene, newScene);
        SceneManager.SetActiveScene(newScene);
        yield return 0;

        // Clean up old scenes

        SceneManager.UnloadScene(oldScene);
        //SceneManager.UnloadScene(tempScene);
        yield return 0;
        
        // Spawn player

        if (spawnPoint != null && spawnPoint.Length > 0) {
            GameObject.Find(spawnPoint).GetComponent<SpawnPoint>().Spawn();
        } else {
            GameObject.FindObjectOfType<SpawnPoint>().Spawn(); // Use the first one by default
        }

        // Handle callback

        if (callback != null) {
            callback();
        }
    }

    public IEnumerator WaitForLoad(Scene scene) {
        while (scene.isLoaded == false) {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }
}