using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    public string sayText;

    private GameObject player;

    private float maxDistance = 7.5f;
    private float maxAngle = 30f;

	void Start () {
        player = GameObject.Find("Player");
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            float angle = Vector3.Angle(player.transform.forward, transform.position - player.transform.position);
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if(angle < maxAngle && distance < maxDistance) {
                if (sayText != null) {
                    player.GetComponent<TankDialog>().Say(sayText);
                }
            }
        }
	}
}
