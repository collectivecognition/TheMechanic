using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    private float followDistance = 80f;

    void Update () {
        if (GameManager.Instance.player != null) {
            transform.position = GameManager.Instance.player.transform.position + Vector3.up * followDistance - Vector3.right * (followDistance + 20f) - Vector3.forward * (followDistance + 20f);
        }
    }
}
