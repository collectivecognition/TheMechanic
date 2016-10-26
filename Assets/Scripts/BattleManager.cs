using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class BattleManager : Singleton<BattleManager> {
    public bool BattleActive { get { return battleActive; } }
    private bool battleActive;
    private Scene scene;

    void Update() {
        CheckForEndOfBattle();

        // FIXME: Test code

        if (Input.GetKeyDown(KeyCode.Z)) { // Start battle
            StartBattle();
        }
    }

    public void StartBattle() {
        battleActive = true;
       
        SceneManager.LoadScene("DesertBattle", LoadSceneMode.Additive);
        scene = SceneManager.GetSceneByName("DesertBattle");
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
            GameManager.Instance.gameActive = true;
            SceneManager.UnloadScene(scene);
        });

        InventoryManager.Instance.inventory.AddItemByName("MachineGunItem");

        Projectile[] projectiles = FindObjectsOfType<Projectile>();
        foreach(Projectile projectile in projectiles) {
            projectile.Remove();
        }
    }
}
