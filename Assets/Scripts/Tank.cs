using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {
    void Start() {
        if (name == "Player") {
            DontDestroyOnLoad(gameObject);
        }
    }
}