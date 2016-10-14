using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    public string sayText;
    public string sceneToLoad;
    public string spawnPoint;
    public bool triggerOnCollide = false;

    private GameObject player;

    private float maxDistance = 7.5f;
    private float maxAngle = 30f;

	void Awake () {
        player = GameObject.Find("Shared/Player");
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            float angle = Vector3.Angle(player.transform.forward, transform.position - player.transform.position);
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if(angle < maxAngle && distance < maxDistance) {
                TriggerInteraction();
            }
        }
	}

    void OnTriggerEnter(Collider other) {
        if(triggerOnCollide && other.name == "Player") {
            TriggerInteraction();
        }
    }

    private void TriggerInteraction() {
        if (sayText != null && sayText.Length > 0) {
            player.GetComponent<TankDialog>().Say(sayText);
        }

        if (sceneToLoad != null) {
            GameManager.Instance.LoadScene(sceneToLoad, () => {
                if (spawnPoint != null && spawnPoint.Length > 0) {
                    GameObject.Find(spawnPoint).GetComponent<SpawnPoint>().Spawn(player);
                }
            });
        }
    }
}
