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
        Plane playerPlane = new Plane(Vector3.up, turretTransform.position);

        // Generate a ray from the cursor position
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist)) {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - turretTransform.position);
            
            // Smoothly rotate towards the target point.
            turretTransform.rotation = Quaternion.Slerp(turretTransform.rotation, targetRotation, 30f * Time.deltaTime);

            Vector3 rayPos = turretTransform.position + Vector3.up * 5f;
            // Debug.DrawRay(rayPos, turretTransform.forward * 1000f, Color.red);
        }
    }

    public void AimAt(GameObject target) {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turretTransform.rotation = lookRotation;
    }
}
