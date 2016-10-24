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

    private float maxDistance = 5f;
    private float maxAngle = 90f;
    private int triggerCount = 0;
    private bool triggered = false;
    private Color[] childEmissionColors;

    private Collider col;
    
    void Start() {
        if (triggerOnLoad) {
            TriggerInteraction();
        }

        childEmissionColors = new Color[transform.childCount];

        col = GetComponent<Collider>();
    }
	
	void Update () {
        if (!GameManager.Instance.gameActive) return;

        if (triggerOnInteract) {
            float angle = Vector3.Angle(GameManager.Instance.player.transform.forward, transform.position - GameManager.Instance.player.transform.position);
            Vector3 closestPoint = col.ClosestPointOnBounds(GameManager.Instance.player.transform.position);
            float distance = Vector3.Distance(closestPoint, GameManager.Instance.player.transform.position);

            if(angle < maxAngle && distance < maxDistance) {
                if (!triggered) {
                    for(int ii = 0; ii < transform.childCount; ii++) {
                        Renderer childRenderer = transform.GetChild(ii).GetComponent<Renderer>();
                        Color childColor = childRenderer.material.GetColor("_Color");
                        childEmissionColors[ii] = childColor;
                        childRenderer.material.SetColor("_Color", new Color(5f, 5f, 5f, 1f));
                    }
                    triggered = true;
                }

                if (Input.GetKeyDown(KeyCode.Space)) {
                    TriggerInteraction();
                }
            }else {
                if (triggered) {
                    triggered = false;
                    for (int ii = 0; ii < transform.childCount; ii++) {
                        Renderer childRenderer = transform.GetChild(ii).GetComponent<Renderer>();
                        childRenderer.material.SetColor("_Color", childEmissionColors[ii]);
                    }
                }
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
