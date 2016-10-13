using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    private GameObject player;

    private float followDistance = 80f;

    void Start () {
        player = GameObject.Find("Player");

        // Keep this in memory between scene loads

        DontDestroyOnLoad(gameObject);
    }

    void Update () {
        transform.position = player.transform.position + Vector3.up * followDistance - Vector3.right * (followDistance + 20f) - Vector3.forward * (followDistance + 20f);
	}
}
