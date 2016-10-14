using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class Shared : MonoBehaviour {
    bool vrMode = false;

    void Awake() {
        // Keep this in memory between scene loads

        DontDestroyOnLoad(gameObject);

        if (vrMode) {
            VRSettings.enabled = true;
        }else {
            VRSettings.enabled = false;
        }
        
    }

    void Update() {

    }
}
