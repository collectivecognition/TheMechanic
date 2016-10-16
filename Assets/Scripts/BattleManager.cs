using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class BattleManager : Singleton<BattleManager> {
    public bool BattleActive { get { return battleActive; } }
    private bool battleActive;

    private Vector3 originalPlayerPosition;
    private Quaternion originalPlayerRotation;
    private string originalPlayerScene;

    void Update() {
        CheckForEndOfBattle();

        // FIXME: Test code

        if (Input.GetKeyDown(KeyCode.Z)) { // Start battle
            StartBattle();
        }
    }

    public void StartBattle() {
        battleActive = true;

        // Save player transform

        Transform t = GameManager.Instance.player.transform;

        originalPlayerPosition = t.position;
        originalPlayerRotation = t.rotation;
        originalPlayerScene = SceneManager.GetActiveScene().name;
        Debug.Log("Scene: " + originalPlayerScene);

        // Load the battle scene before initializing the battle

        GameManager.Instance.LoadScene("TestBattle", null, () => {
        });
    }

    private void CheckForEndOfBattle() {
        if (battleActive) {
            int activeEnemies = GameObject.FindGameObjectsWithTag("Enemy").Count<GameObject>();
            if (activeEnemies == 0) {
                EndBattle();
            }
        }
    }

    private void EndBattle() {
        battleActive = false;
        
        GameManager.Instance.LoadScene(originalPlayerScene, null, () => {
            GameManager.Instance.player.transform.position = originalPlayerPosition;
            GameManager.Instance.player.transform.rotation = originalPlayerRotation;
        });

        GameManager.Instance.gameActive = true;
    }
}
