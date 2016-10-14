using UnityEngine;
using System.Collections;

public class TankTurret : MonoBehaviour {
    [HideInInspector]
    public bool controllable = true;

    private Camera camera;
    private float turnSpeed = 50f;
    private Transform turretTransform;

    private void Start() {
        turretTransform = transform.Find("Turret");
        camera = GameObject.Find("Shared/Camera/Camera").GetComponent<Camera>();
    }

    private void Update() {
        if (controllable && name == "Player") {
            // turretTransform.Rotate(Vector3.up * Input.GetAxis("Right Stick Horizontal") * turnSpeed * Time.deltaTime);
            //Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, turretTransform.position.z));
            //mouseWorldPosition.z = turretTransform.position.z;
            //turretTransform.LookAt(Vector3.zero);
            // turretTransform.position = mouseWorldPosition;
            //turretTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mouseWorldPosition.y - turretTransform.position.y), (mouseWorldPosition.x - turretTransform.position.x)) * Mathf.Rad2Deg);
        }
    }

    private void FixedUpdate() {
        if (controllable && name == "Player") {
            Plane playerPlane = new Plane(Vector3.up, turretTransform.position);
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;
            if (playerPlane.Raycast(ray, out hitdist)) {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - turretTransform.position);
                turretTransform.rotation = Quaternion.Slerp(turretTransform.rotation, targetRotation, 30f * Time.deltaTime);
                Vector3 rayPos = turretTransform.position + Vector3.up * 5f;
            }
        }
    }

    public void AimAt(GameObject target) {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turretTransform.rotation = lookRotation;
    }
}
