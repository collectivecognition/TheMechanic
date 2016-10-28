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

        float h = Input.GetAxis("TurretHorizontal");
        float v = Input.GetAxis("TurretVertical");

        if (h != 0 || v != 0) {
            Vector3 targetPoint = Vector3.zero;

            targetPoint += Vector3.right * h;
            targetPoint += Vector3.forward * v;

            //if (targetPoint == Vector3.zero) {
            //    turretTransform.localRotation = Quaternion.Slerp(turretTransform.localRotation, bodyTransform.rotation * Quaternion.Euler(0, -270, 0), turnSpeed * Time.deltaTime);
            //} else {
            //    AimAt(targetPoint);
            //}
        }else {
            Plane plane = new Plane(Vector3.up, new Vector3(0f, 15f, 0f));
            float hit;
            Ray ray = GameManager.Instance.renderCamera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out hit)) {
                Vector3 target = ray.GetPoint(hit);
                Vector3 current = turretTransform.position;
                current.y = 5f;

                Quaternion targetRotation = Quaternion.LookRotation(target - current);
                targetRotation *= Quaternion.Euler(0, 90, 0);
                turretTransform.localRotation = Quaternion.Slerp(turretTransform.localRotation, targetRotation, turnSpeed * Time.deltaTime);
            }
        }
    }

    public void AimAt(Vector3 target) {
        //target = turretTransform.position + target;
        Quaternion targetRotation = Quaternion.LookRotation(target - turretTransform.position);
        // targetRotation *= Quaternion.Euler(0, 135, 0);
        turretTransform.localRotation = Quaternion.Slerp(turretTransform.localRotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public bool IsAimingAt(Vector3 target) {
        float angle = Vector3.Angle(turretTransform.transform.forward, turretTransform.position - target);
        return angle > 180 - 10 && angle < 180 + 10;
    }
}
