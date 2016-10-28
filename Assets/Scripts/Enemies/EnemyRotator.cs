using UnityEngine;
using System.Collections;

public class EnemyRotator : Enemy {
    private GameObject projectilePrefab;

    private float projectileSpeed = 7f;
    private float projectileInterval = 0.4f;
    private float projectileAngle = 0f;
    private float projectileAngleInterval = 30f;
    private float lastProjectileTime;

    void Start() {
        base.Start();

        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");

        iTween.MoveAdd(gameObject, iTween.Hash("amount", gameObject.transform.up * 2f, "time", 1, "loopType", iTween.LoopType.pingPong, "delay", 0, "easetype", iTween.EaseType.easeInOutQuad));
        iTween.RotateAdd(gameObject, iTween.Hash("y", 360f, "time", (360f / projectileAngleInterval) * projectileInterval, "delay", 0, "loopType", "loop", "easeType", iTween.EaseType.linear));

        lastProjectileTime = Time.fixedTime;
    }

    void Update() {
        if (!GameManager.Instance.gameActive) return;

        if (Time.fixedTime - lastProjectileTime >= projectileInterval) {
            GameObject projectileObject = (GameObject)Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectileObject.layer = LayerMask.NameToLayer("EnemyProjectiles");
            projectile.direction = Quaternion.AngleAxis(projectileAngle, Vector3.up) * Vector3.forward;
            projectile.speed = projectileSpeed;
            projectile.minDamage = 20f;
            projectile.maxDamage = 50f;

            lastProjectileTime = Time.fixedTime;
            projectileAngle += projectileAngleInterval;
            if (projectileAngleInterval > 360f) {
                projectileAngleInterval = 0f;
            }
        }
    }
}
