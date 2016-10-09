using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Transform player;

    private float followDistance = 25f;
    private float elevation = 15f;
    private float followSpeed = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void Update () {
        transform.LookAt(player.transform);

        //if (Vector3.Distance(transform.position, player.position) > followDistance) {
            Vector3 targetPosition = player.position;
            Transform turretTransform = player.transform.Find("Tank/Turret").transform;
            targetPosition = player.transform.position + (turretTransform.right * followDistance) + (Vector3.up * elevation);
            Debug.Log(turretTransform.rotation);
            // targetPosition.y = player.position.y + elevation;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSpeed);
        //}
	}
}
