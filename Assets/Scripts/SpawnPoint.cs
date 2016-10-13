using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
    public bool useRotation = false;

	void Start() {
        
        // Hide on startup

        transform.localScale = new Vector3(0, 0, 0);
    }

    public void Spawn(GameObject g) {
        g.transform.position = transform.position;

        if (useRotation) { 
            g.transform.rotation = transform.rotation;
        }
    }
}
