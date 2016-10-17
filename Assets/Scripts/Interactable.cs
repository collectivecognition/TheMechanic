using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    public string[] sayText;
    public string playCutscene;
    public string loadScene;
    public string sceneSpawnPoint;
    public bool oneShot = true;
    public bool triggerOnCollide = false;
    public bool triggerOnLoad = false;
    public bool triggerOnInteract = false;
    public bool startBattle = false;

    private float maxDistance = 7.5f;
    private float maxAngle = 30f;
    private int triggerCount = 0;

    void Start() {
        if (triggerOnLoad) {
            TriggerInteraction();
        }
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && triggerOnInteract) {
            float angle = Vector3.Angle(GameManager.Instance.player.transform.forward, transform.position - GameManager.Instance.player.transform.position);
            float distance = Vector3.Distance(GameManager.Instance.player.transform.position, transform.position);

            if(angle < maxAngle && distance < maxDistance) {
                TriggerInteraction();
            }
        }
	}

    void OnTriggerEnter(Collider other) {
        if(triggerOnCollide && other.tag == "Player") {
            TriggerInteraction();
        }
    }

    private void TriggerInteraction() {
        if (oneShot && triggerCount > 0) {
            return;
        }

        triggerCount++;

        if (sayText != null && sayText.Length > 0) {
            CutsceneEvent[] cutscene = new CutsceneEvent[sayText.Length];
            for (int ii = 0; ii < sayText.Length; ii++){
                cutscene[ii] = new CutsceneDialogEvent(sayText[ii]);
            };
            CutsceneManager.Instance.Play(cutscene);
        }

        if (loadScene != null && loadScene.Length > 0) {
            GameManager.Instance.LoadScene(loadScene, sceneSpawnPoint);
        }

        if (startBattle) {
            BattleManager.Instance.StartBattle();
        }
    }
}
