using UnityEngine;
using System.Collections;

public class EnemyBouncer : Enemy {
    private float speed = 40f;
    private Vector3 direction;

    void Start() {
        base.Start();
        direction = transform.forward;
        iTween.RotateAdd(gameObject, iTween.Hash("y", 360f, "time", 1f, "delay", 0, "loopType", "loop", "easeType", iTween.EaseType.linear));
    }

    void Update() {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag != "Player" && collider.tag != "Enemy" && collider.tag != "Projectile" && collider.tag != "EnemyProjectile") {
            direction = -direction;
        }
    }
}
