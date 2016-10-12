using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Transform player;

    private float followDistance = 25f;
    private float elevation = 15f;
    private float followSpeed = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void Update () {
        //transform.LookAt(player.transform);

        //Vector3 targetPosition = player.position;
        //Transform turretTransform = player.transform.Find("Turret").transform;
        //targetPosition = player.transform.position + (-turretTransform.forward * followDistance) + (Vector3.up * elevation);
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSpeed);

        transform.position = player.transform.position + Vector3.up * 30 - Vector3.right * 20 - Vector3.forward * 20;
	}
}
