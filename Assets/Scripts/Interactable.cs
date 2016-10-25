using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {
    public List<string> sayText = new List<string>();
    public List<string> inventoryItems = new List<string>();
    public string playCutscene;
    public bool oneShot = true;
    public bool triggerOnCollide = false;
    public bool triggerOnLoad = false;
    public bool triggerOnInteract = false;
    public bool startBattle = false;

    private string id;
    private float maxDistance = 5f;
    private float maxAngle = 90f;
    private bool triggered = false;
    private Color[] childEmissionColors;
    private Renderer[] childRenderers;

    private Collider col;
    
    void Start() {
        col = GetComponent<Collider>();

        id = GameManager.Instance.currentSceneName + "@" + name; // Scene name + interactable name

        if (!GameManager.Instance.interactableTriggerCounts.ContainsKey(id)) {
            GameManager.Instance.interactableTriggerCounts.Add(id, 0);
        }

        childRenderers = transform.GetComponentsInChildren<Renderer>();
        childEmissionColors = new Color[childRenderers.Length];

        if (triggerOnLoad) {
            TriggerInteraction();
        }
    }
	
	void Update () {
        if (!GameManager.Instance.gameActive) return;

        // Handle manual interaction with objects

        if (triggerOnInteract) {
            float angle = Vector3.Angle(GameManager.Instance.player.transform.forward, transform.position - GameManager.Instance.player.transform.position);
            Vector3 closestPoint = col.ClosestPointOnBounds(GameManager.Instance.player.transform.position);
            float distance = Vector3.Distance(closestPoint, GameManager.Instance.player.transform.position);

            if(angle < maxAngle && distance < maxDistance) {
                if (!triggered) {
                    for (int ii = 0; ii < childRenderers.Length; ii++) {
                        Renderer childRenderer = childRenderers[ii];
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
                    for (int ii = 0; ii < childRenderers.Length; ii++) {
                        Renderer childRenderer = childRenderers[ii];
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

        // Make sure one-shot interactions only happen once

        if (oneShot && GameManager.Instance.interactableTriggerCounts[id] > 0) {
            return;
        }

        // Fire off event so other scripts can respond

        SendMessage("OnInteraction");

        // Create an empty list of cutscene events

        List<CutsceneEvent> cutsceneEvents = new List<CutsceneEvent>();

        // Handle dialogue events

        if (sayText != null && sayText.Count > 0) {
            for (int ii = 0; ii < sayText.Count; ii++) {
                cutsceneEvents.Add(new CutsceneDialogEvent(sayText[ii]));
            };
        }

        // Handle handing out inventory items and creating a cutscene event to let the player know they picked something up

        if (GameManager.Instance.interactableTriggerCounts[id] == 0 && inventoryItems.Count > 0) { // Items can only be picked up once
            string eventText = "You picked up:\n";
            foreach (string itemName in inventoryItems) {
                InventoryItem inventoryItem = InventoryManager.Instance.inventory.AddItemByName(itemName);
                eventText += "\n " + inventoryItem.name;
            }
            cutsceneEvents.Add(new CutsceneDialogEvent(eventText));
        }

        // Increment trigger count

        GameManager.Instance.interactableTriggerCounts[id]++;

        // Play cutscene

        if (cutsceneEvents.Count > 0) {
            CutsceneManager.Instance.Play(cutsceneEvents.ToArray());
        }

        // Start battle
        // FIXME: Battle name should be configurable

        if (startBattle) {
            BattleManager.Instance.StartBattle();
        }
    }
}
