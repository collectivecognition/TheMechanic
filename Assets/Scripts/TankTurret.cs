using UnityEngine;
using System.Collections;

public class TankTurret : MonoBehaviour {
    private Camera camera;
    private float turnSpeed = 1f;
    private Transform turretTransform;

    private void Start() {
        turretTransform = transform.Find("Turret");
        camera = GameObject.Find("Shared/Camera/Camera").GetComponent<Camera>();
    }

    private void Update() {
        if (GameManager.Instance.gameActive && name == "Player") {
            // turretTransform.Rotate(Vector3.up * Input.GetAxis("Right Stick Horizontal") * turnSpeed * Time.deltaTime);
            //Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, turretTransform.position.z));
            //mouseWorldPosition.z = turretTransform.position.z;
            //turretTransform.LookAt(Vector3.zero);
            // turretTransform.position = mouseWorldPosition;
            //turretTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mouseWorldPosition.y - turretTransform.position.y), (mouseWorldPosition.x - turretTransform.position.x)) * Mathf.Rad2Deg);
        }
    }

    private void FixedUpdate() {
        if (GameManager.Instance.gameActive && BattleManager.Instance.BattleActive && name == "Player") {
            Plane playerPlane = new Plane(Vector3.up, turretTransform.position);
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;
            if (playerPlane.Raycast(ray, out hitdist)) {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                AimAt(targetPoint);
            }
        }
    }

    public void AimAt(Vector3 target) {
        Quaternion targetRotation = Quaternion.LookRotation(target - turretTransform.position);
        turretTransform.rotation = Quaternion.Slerp(turretTransform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public bool IsAimingAt(Vector3 target) {
        float angle = Vector3.Angle(turretTransform.transform.forward, turretTransform.position - target);
        return angle > 180 - 10 && angle < 180 + 10;
    }
}
