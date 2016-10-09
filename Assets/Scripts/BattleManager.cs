using UnityEngine;
using System.Collections.Generic;

public class BattleManager : Singleton<BattleManager> {
    static GameObject player;
    static GameObject battle;

    void Start() {
        player = GameObject.Find("Player");
        battle = GameObject.Find("Battle");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            StartBattle();
        }
    }

    public void StartBattle() {
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        GameObject[] spawnPoints = battle.FindChildrenByName("SpawnPoint");
        GameObject spawnPoint = spawnPoints.Random();
        player.transform.position = spawnPoint.transform.position;
    }
}
