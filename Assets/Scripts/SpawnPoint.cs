using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
    public bool useRotation = false;
    public string playerPrefabName = "PlayerTank";

	void Start() {
        
        // Hide on startup

        transform.localScale = new Vector3(0, 0, 0);
    }

    public void Spawn() {
        GameObject playerPrefab = (GameObject)Resources.Load("Prefabs/" + playerPrefabName);
        GameObject player = GameObject.Instantiate(playerPrefab);

        player.transform.position = transform.position;
        GameManager.Instance.player = player;

        if (useRotation) {
            player.transform.rotation = transform.rotation;
        }
    }
}
