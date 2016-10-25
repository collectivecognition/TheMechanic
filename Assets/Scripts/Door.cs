using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public string loadScene;
    public string sceneSpawnPoint;
    public bool locked = false;
    public string keyItemName;
    public string lockMessage;
    public string unlockMessage;

    public void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "Player") {
            if (locked) {
                CutsceneEvent[] cutscene = new CutsceneEvent[1];

                if (InventoryManager.Instance.inventory.HasItem(keyItemName)) {
                    locked = false;
                    cutscene[0] = new CutsceneDialogEvent(unlockMessage);
                }else {
                    cutscene[0] = new CutsceneDialogEvent(lockMessage);
                }

                CutsceneManager.Instance.Play(cutscene, () => {
                    if (!locked) {
                        Debug.Log("Loading scene");
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
