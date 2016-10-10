using UnityEngine;
using System.Collections;

public class TankTurret : MonoBehaviour {
    [HideInInspector]
    public bool controllable = true;

    private float turnSpeed = 50f;
    private Transform turretTransform;

    private void Start() {
        turretTransform = transform.Find("Turret");
    }

    private void Update() {
        if(controllable && name == "Player") {
            turretTransform.Rotate(Vector3.up * Input.GetAxis("Right Stick Horizontal") * turnSpeed * Time.deltaTime);
        }
    }

    public void AimAt(GameObject target) {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turretTransform.rotation = lookRotation;
    }
}
