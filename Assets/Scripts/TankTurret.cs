using UnityEngine;
using System.Collections;

public class TankTurret : MonoBehaviour {
    private float turnSpeed = 20f;
    private Transform turretTransform;
    private Transform bodyTransform;
    private Vector3 oldMousePos = Vector3.zero;

    private void Start() {
        turretTransform = transform.Find("Turret");
        bodyTransform = transform.Find("TankBody");
    }

    private void FixedUpdate() {
        if (!GameManager.Instance.gameActive) return;

        if (!BattleManager.Instance.BattleActive) {
            turretTransform.localRotation = bodyTransform.localRotation;
            Cursor.visible = false;
            return;
        }

        float h = Input.GetAxis("TurretHorizontal");
        float v = Input.GetAxis("TurretVertical");

        // Keyboard and analog stick aiming

        if (h != 0 || v != 0) {
            Cursor.visible = false;
            Vector3 targetPoint = Vector3.zero;

            targetPoint += Vector3.right * v;
            targetPoint += Vector3.forward * h;

            if (targetPoint == Vector3.zero) {
                turretTransform.localRotation = Quaternion.Slerp(turretTransform.localRotation, bodyTransform.rotation * Quaternion.Euler(0, -270, 0), turnSpeed * Time.deltaTime);
            } else {
                targetPoint = turretTransform.position + targetPoint;
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - turretTransform.position);
                targetRotation *= Quaternion.Euler(0, 135, 0);
                turretTransform.localRotation = Quaternion.Slerp(turretTransform.localRotation, targetRotation, turnSpeed * Time.deltaTime);
            }

        // Mouse aiming

        } else if(oldMousePos != Input.mousePosition) { // Only rotate if player is actually using mouse
            oldMousePos = Input.mousePosition;
            Cursor.visible = true;

            Plane plane = new Plane(Vector3.up, new Vector3(0f, 15f, 0f)); // 15f is the turret height
            float hit;
            Ray ray = GameManager.Instance.renderCamera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out hit)) {
                Vector3 target = ray.GetPoint(hit);
                Vector3 current = turretTransform.position;
                current.y = 15f;

                Quaternion targetRotation = Quaternion.LookRotation(target - current);
                targetRotation *= Quaternion.Euler(0, 45, 0);
                turretTransform.localRotation = Quaternion.Slerp(turretTransform.localRotation, targetRotation, turnSpeed * Time.deltaTime);
            }
        }
    }
}
