using UnityEngine;
using System.Collections;

public class TankTurret : MonoBehaviour {
    private float turnSpeed = 20f;
    private Transform turretTransform;
    private Transform bodyTransform;

    private void Start() {
        turretTransform = transform.Find("Turret");
        bodyTransform = transform.Find("TankBody");
    }

    private void FixedUpdate() {
        if (!GameManager.Instance.gameActive) return;

        Vector3 targetPoint = Vector3.zero;

        targetPoint += Vector3.right * Input.GetAxis("TurretHorizontal");
        targetPoint += Vector3.forward * Input.GetAxis("TurretVertical");

        if (targetPoint == Vector3.zero) {
            turretTransform.localRotation = Quaternion.Slerp(turretTransform.localRotation, bodyTransform.rotation * Quaternion.Euler(0, -270, 0), turnSpeed * Time.deltaTime);
        } else {
            AimAt(targetPoint);
        }
    }

    public void AimAt(Vector3 target) {
        target = turretTransform.position + target;
        Quaternion targetRotation = Quaternion.LookRotation(target - turretTransform.position);
        targetRotation *= Quaternion.Euler(0, 135, 0);
        turretTransform.localRotation = Quaternion.Slerp(turretTransform.localRotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public bool IsAimingAt(Vector3 target) {
        float angle = Vector3.Angle(turretTransform.transform.forward, turretTransform.position - target);
        return angle > 180 - 10 && angle < 180 + 10;
    }
}
