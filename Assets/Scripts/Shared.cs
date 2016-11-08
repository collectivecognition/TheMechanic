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

            gameObject.AddComponent<BattleManager>();
            gameObject.AddComponent<CutsceneManager>();
            gameObject.AddComponent<EnemyManager>();
            gameObject.AddComponent<GameManager>();
            gameObject.AddComponent<CameraManager>();
            gameObject.AddComponent<UIManager>();
            gameObject.AddComponent<InventoryManager>();
            gameObject.AddComponent<PlayerManager>();
        }
    }
}
