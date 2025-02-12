﻿using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public string loadScene;
    public string sceneSpawnPoint;
    public bool locked = false;
    public string keyItemName;
    public string lockMessage;
    public string unlockMessage;
    public bool forHumans = true;
    public bool forTanks = false;

    public void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "Player") {
            if ((collision.collider.name.Contains("PlayerHuman") && forHumans) || collision.collider.name.Contains("PlayerTank") && forTanks) {
                if (locked) {
                    CutsceneEvent[] cutscene = new CutsceneEvent[1];

                    if (InventoryManager.Instance.inventory.HasItem(keyItemName)) {
                        locked = false;
                        cutscene[0] = new CutsceneDialogueEvent(unlockMessage);
                    } else {
                        cutscene[0] = new CutsceneDialogueEvent(lockMessage);
                    }

                    CutsceneManager.Instance.Play(cutscene, () => {
                        if (!locked) {
                            GameManager.Instance.LoadScene(loadScene, sceneSpawnPoint);
                        }
                    });
                } else {
                    if (loadScene != null && loadScene.Length > 0) {
                        GameManager.Instance.LoadScene(loadScene, sceneSpawnPoint);
                    }
                }
            }
        }
    }
}
