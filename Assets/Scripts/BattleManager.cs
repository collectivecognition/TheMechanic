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

        // Load the battle scene before initializing the battle

        GameManager.Instance.LoadScene("DesertBattle", null, () => {
        });
    }

    private void CheckForEndOfBattle() {
        if (battleActive) {
            int activeEnemies = EnemyManager.Instance.enemies.Count;
            if (activeEnemies == 0) {
                EndBattle();
            }
        }
    }

    private void EndBattle() {
        battleActive = false;

        PostBattleManager.Instance.Do(100, new InventoryItem[] {
            new MachineGunItem()
        }, () => {
            GameManager.Instance.LoadScene(originalPlayerScene, null, () => {
                GameManager.Instance.player.transform.position = originalPlayerPosition;
                GameManager.Instance.player.transform.rotation = originalPlayerRotation;
            });
        });

        InventoryManager.Instance.inventory.AddItem(new MachineGunItem());

        //GameObject loot = Resources.Load<GameObject>("Prefabs/Powerup");
        //GameObject.Instantiate(loot, new Vector3(0, 10, 0), Quaternion.identity);
        //GameObject.Instantiate(loot, new Vector3(10, 7, 0), Quaternion.identity);
        //GameObject.Instantiate(loot, new Vector3(-10, 11, 0), Quaternion.identity);

        GameManager.Instance.gameActive = true;
    }
}
