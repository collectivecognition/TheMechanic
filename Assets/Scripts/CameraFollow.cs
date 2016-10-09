using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Transform player;

    private float followDistance = 30f;
    private float elevation = 25f;
    private float followSpeed = 10f;

    void Update () {
        transform.LookAt(player.transform);

        if (Vector3.Distance(transform.position, player.position) > followDistance) {
            Vector3 targetPosition = player.position;
            targetPosition.y = player.position.y + elevation;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
	}
}
