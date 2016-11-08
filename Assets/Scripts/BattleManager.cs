using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

public class BattleManager : Singleton<BattleManager> {
    public bool BattleActive { get { return battleActive; } }
    private bool battleActive;
    private Scene scene;

    void Update() {
        CheckForEndOfBattle();
    }

    public void StartBattle(string battleName) {
        SceneManager.LoadScene(battleName, LoadSceneMode.Additive);
        scene = SceneManager.GetSceneByName(battleName);
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
        GameManager.Instance.postBattleUI.Open(100, new InventoryItem[] {
            new MachineGunItem()
        }, () => {

            // Give loot
            // FIXME

            InventoryManager.Instance.inventory.AddItemByName("MachineGunItem");

            // Clean up

            BattleShield[] battleShields = FindObjectsOfType<BattleShield>();
            foreach (BattleShield battleShield in battleShields) {
                battleShield.OnBattleEnd();
            }

            Projectile[] projectiles = FindObjectsOfType<Projectile>();
            foreach (Projectile projectile in projectiles) {
                projectile.Remove();
            }

            GameObject[] gibs = GameObject.FindGameObjectsWithTag("Gib");
            foreach (GameObject gib in gibs) {
                iTween.FadeTo(gib, iTween.Hash("alpha", 0f, "time", 0.5f));
                GameObject.Destroy(gib, 0.5f);
            }

            // Finalize

            StartCoroutine(FinalizeBattleAfter(10f));
        });
    }

    IEnumerator FinalizeBattleAfter(float seconds) {
        yield return new WaitForSeconds(seconds);

        GameManager.Instance.gameActive = true;
        SceneManager.UnloadScene(scene);
    }
}
