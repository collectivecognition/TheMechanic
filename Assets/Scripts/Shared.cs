using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class Shared : Singleton<Shared> {
    static bool vrMode = false;
    static bool initialized = false;

    void Awake() {
        if (!initialized) {
            initialized = true;

            // Keep this in memory between scene loads

            DontDestroyOnLoad(gameObject);

            // Handle VR Mode

            if (vrMode) {
                VRSettings.enabled = true;
            } else {
                VRSettings.enabled = false;
            }

            // Instantiate managers
            // WARNING: Order matters here

            gameObject.AddComponent<PlayerManager>();
            gameObject.AddComponent<BattleManager>();
            gameObject.AddComponent<CameraManager>();
            gameObject.AddComponent<CutsceneManager>();
            gameObject.AddComponent<EnemyManager>();
            gameObject.AddComponent<GameManager>();
            gameObject.AddComponent<UIManager>();
            gameObject.AddComponent<PostBattleManager>();
            gameObject.AddComponent<InventoryManager>();

            // Setup camera

            GameObject camera = Instantiate((GameObject)Resources.Load("Prefabs/Camera"));
            camera.transform.SetParent(transform);
            GameManager.Instance.cameraObject = camera;
            GameManager.Instance.renderCamera = camera.transform.Find("RenderCameraHarness/RenderCamera").GetComponent<Camera>();

            // Set up some other references (FIXME: Maybe this goes into GameManager?)

            GameManager.Instance.dialogue = camera.GetComponentInChildren<DialogueUI>();

            // Spawn player
            // FIXME: What if there is more than one spawn point in a scene?

            GameObject.FindObjectOfType<SpawnPoint>().Spawn();
        }
    }
}
