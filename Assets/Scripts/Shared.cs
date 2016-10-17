using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class Shared : Singleton<Shared> {
    bool vrMode = false;

    void Awake() {
        // Keep this in memory between scene loads

        DontDestroyOnLoad(gameObject);

        if (vrMode) {
            VRSettings.enabled = true;
        }else {
            VRSettings.enabled = false;
        }

        gameObject.AddComponent<BattleManager>();
        gameObject.AddComponent<CutsceneManager>();
        gameObject.AddComponent<EnemyManager>();
        gameObject.AddComponent<GameManager>();

        GameObject camera = Instantiate((GameObject)Resources.Load("Prefabs/Camera"));
        camera.transform.SetParent(transform);
        GameManager.Instance.cam = camera.GetComponentInChildren<Camera>();

        GameManager.Instance.dialogue = camera.GetComponentInChildren<Dialogue>();
    }
}
